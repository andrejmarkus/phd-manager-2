using LdapForNet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;

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

        public async Task<string?> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await UserManager.GetRolesAsync(user);
            return roles.First();
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser is null) return null;
            return await GetUserRoleAsync(currentUser);
        }

        public async Task AddStudentToUser(ApplicationUser user, Student student)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Students.Add(student);
            await UserManager.UpdateAsync(unusedUser);
        }

        public async Task AddAdminToUser(ApplicationUser user, Admin admin)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Admin = admin;
            await UserManager.UpdateAsync(unusedUser);
        }

        public async Task AddTeacherToUser(ApplicationUser user, Teacher teacher)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Teacher = teacher;
            await UserManager.UpdateAsync(unusedUser);
        }

        public async Task AddExternalToUser(ApplicationUser user, ExternalTeacher externalTeacher)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is null) return;

            unusedUser.Teacher = externalTeacher;
            await UserManager.UpdateAsync(unusedUser);
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

        public async Task ClearRegistrations()
        {
            var registrations = (await UnitOfWork.Registrations.GetAllAsync())?.Where(r => r.Expiration < DateTime.Now);

            if (registrations is null) return;

            foreach (var registration in registrations)
            {
                UnitOfWork.Registrations.Delete(registration);
            }
            await UnitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);

            if (unusedUser is null) return;

            await UserManager.DeleteAsync(unusedUser);
        }

        public async Task<ApplicationUser> RegisterLdapUserWithoutPasswordAsync(LdapEntry entry, string role)
        {
            var user = await CreateLdapUserAsync(entry);
            await UserManager.CreateAsync(user);
            await UserManager.AddToRoleAsync(user, role);

            return user;
        }

        public async Task<ApplicationUser> RegisterLdapUserAsync(LdapEntry entry, string password, string role)
        {
            var user = await CreateLdapUserAsync(entry);
            await UserManager.CreateAsync(user, password);
            await UserManager.AddToRoleAsync(user, role);

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

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!UserManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)UserStore;
        }

        private ApplicationUser CreateUser()
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
    }
}
