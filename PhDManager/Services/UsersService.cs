using DocumentFormat.OpenXml.Packaging;
using LdapForNet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Data.Migrations;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;
using System.Security.Claims;

namespace PhDManager.Services
{
    public class UsersService(
        IUnitOfWork UnitOfWork,
        IUserStore<ApplicationUser> UserStore,
        UserManager<ApplicationUser> UserManager,
        RoleManager<IdentityRole> RoleManager,
        AuthenticationStateProvider AuthenticationStateProvider,
        ActiveDirectoryService ActiveDirectoryService
        )
    {
        public IEnumerable<IdentityRole> Roles => RoleManager.Roles;

        public IEnumerable<ApplicationUser>? GetAll()
        {
            return UserManager.Users;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return await UserManager.GetUserAsync(authState.User);
        }

        public ApplicationUser? GetUserByUsernameAsync(string username)
        {
            return UserManager.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public async Task<string?> GetUserIdAsync(ApplicationUser user)
        {
            return await UserManager.GetUserIdAsync(user);
        }

        public ApplicationUser? GetUserById(string userId)
        {
            return UserManager.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return (await UserManager.GetUserAsync(authState.User))?.Id;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public async Task<string?> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await UserManager.GetRolesAsync(user);
            return roles[0];
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser is null) return null;
            return await GetUserRoleAsync(currentUser);
        }

        public async Task UpdateUserRoleAsync(ApplicationUser user, string role)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is not null)
            {
                var userRoles = await UserManager.GetRolesAsync(unusedUser);
                await UserManager.RemoveFromRolesAsync(unusedUser, userRoles);
                await UserManager.AddToRoleAsync(unusedUser, role);
            }
        }

        public async Task UpdateUsers()
        {
            var users = UserManager.Users.Where(u => !u.IsExternal).ToList();
            foreach (var user in users)
            {
                if (user.UserName is null) continue;

                var entry = await ActiveDirectoryService.SearchUserAsync(user.UserName);

                if (entry is null) continue;

                user.Email = entry.DirectoryAttributes["mail"].GetValue<string>();
                user.UserName = entry.DirectoryAttributes["uid"].GetValue<string>();
                user.DisplayName = entry.DirectoryAttributes["displayName"].GetValue<string>();

                await UserManager.UpdateAsync(user);
            }
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);

            if (unusedUser is null) return;

            if (unusedUser.IsExternal && unusedUser.Email is not null)
            {
                var invitation = await UnitOfWork.Invitations.GetByEmailAsync(unusedUser.Email);
                if (invitation is not null)
                {
                    UnitOfWork.Invitations.Delete(invitation);
                    await UnitOfWork.CompleteAsync();
                }
            }
            await UserManager.DeleteAsync(unusedUser);
        }

        public async Task<ApplicationUser?> RegisterLdapUserWithoutPasswordAsync(LdapEntry entry, string role)
        {
            var user = await CreateLdapUserAsync(entry);
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded) return null;

            await AssignRole(user, role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterLdapUserAsync(LdapEntry entry, string password, string role)
        {
            var user = await CreateLdapUserAsync(entry);
            var result = await UserManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await AssignRole(user, role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterUserAsync(string email, string username, string displayName, string password)
        {
            var invitation = await UnitOfWork.Invitations.GetByEmailAsync(email);
            if (invitation is null) return null;

            var user = await CreateUserAsync(email, username, displayName);
            var result = await UserManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await AssignRole(user, invitation.Role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterUserAsync(string email, ExternalLoginInfo externalLoginInfo)
        {
            var invitation = await UnitOfWork.Invitations.GetByEmailAsync(email);
            if (invitation is null) return null;

            var user = await CreateUserAsync(email, externalLoginInfo);
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded) return null;

            await AssignRole(user, invitation.Role);

            return user;
        }


        private async Task<ApplicationUser> CreateLdapUserAsync(LdapEntry entry)
        {
            var user = CreateUser();
            var mail = entry.DirectoryAttributes["mail"].GetValue<string>();
            var username = entry.DirectoryAttributes["uid"].GetValue<string>();

            await UserStore.SetUserNameAsync(user, username, CancellationToken.None);
            var emailStore = GetEmailStore();
            await emailStore.SetEmailAsync(user, mail, CancellationToken.None);
            user.EmailConfirmed = true;
            user.DisplayName = entry.DirectoryAttributes["displayName"].GetValue<string>();

            return user;
        }

        private async Task<ApplicationUser> CreateUserAsync(string email, string username, string displayName)
        {
            var user = CreateUser();

            await UserStore.SetUserNameAsync(user, username, CancellationToken.None);
            var emailStore = GetEmailStore();
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            user.IsExternal = true;
            user.DisplayName = displayName;

            return user;
        }

        private async Task<ApplicationUser> CreateUserAsync(string email, ExternalLoginInfo externalLoginInfo)
        {
            var user = CreateUser();

            await UserStore.SetUserNameAsync(user, email, CancellationToken.None);
            var emailStore = GetEmailStore();
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            user.IsExternal = true;
            user.DisplayName = externalLoginInfo.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return user;
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!UserManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)UserStore;
        }

        private static ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

        private async Task AssignRole(ApplicationUser user, string role)
        {
            await UserManager.AddToRoleAsync(user, role);

            switch (role)
            {
                case Admin.Role:
                    {
                        await AddAdminToUser(user, new());
                        break;
                    }
                case Student.Role:
                    {
                        await AddStudentToUser(user, new());
                        break;
                    }
                case Teacher.Role:
                    {
                        await AddTeacherToUser(user, new());
                        break;
                    }
                case ExternalTeacher.Role:
                    {
                        await AddExternalTeacherToUser(user, new());
                        break;
                    }
                default: break;
            }
            await UnitOfWork.CompleteAsync();
        }

        private async Task AddStudentToUser(ApplicationUser user, Student student)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Students.Add(student);
            await UserManager.UpdateAsync(unusedUser);
        }

        private async Task AddAdminToUser(ApplicationUser user, Admin admin)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Admin = admin;
            await UserManager.UpdateAsync(unusedUser);
        }

        private async Task AddTeacherToUser(ApplicationUser user, Teacher teacher)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Teacher = teacher;
            await UserManager.UpdateAsync(unusedUser);
        }

        private async Task AddExternalTeacherToUser(ApplicationUser user, ExternalTeacher externalTeacher)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Teacher = externalTeacher;
            await UserManager.UpdateAsync(unusedUser);
        }
    }
}
