using LdapForNet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models;

namespace PhDManager.Services
{
    public class UsersService(IUserStore<ApplicationUser> UserStore, UserManager<ApplicationUser> UserManager, AuthenticationStateProvider AuthenticationStateProvider)
    {
        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return await UserManager.GetUserAsync(authState.User);
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

        public async Task UpdateUserInfo(ApplicationUser user)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            if (unusedUser is not null) {
                unusedUser.DisplayName = user.DisplayName;
                unusedUser.Birthdate = user.Birthdate?.ToUniversalTime();
                unusedUser.Address = user.Address;
                unusedUser.StudyProgram = user.StudyProgram;
                await UserManager.UpdateAsync(unusedUser);
                var token = await UserManager.GenerateChangePhoneNumberTokenAsync(unusedUser, user.PhoneNumber);
                await UserManager.ChangePhoneNumberAsync(unusedUser, user.PhoneNumber, token);
            }
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            var unusedUser = await UserManager.FindByIdAsync(user.Id);
            await UserManager.DeleteAsync(unusedUser);
        }

        public async Task<ApplicationUser> RegisterLdapUserWithoutPasswordAsync(LdapEntry entry)
        {
            var user = await CreateLdapUserAsync(entry);
            var result = await UserManager.CreateAsync(user);
            await UserManager.AddToRoleAsync(user, "User");

            return user;
        }

        public async Task<ApplicationUser> RegisterLdapUserAsync(LdapEntry entry, string password)
        {
            var user = await CreateLdapUserAsync(entry);
            var result = await UserManager.CreateAsync(user, password);
            await UserManager.AddToRoleAsync(user, "User");

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
            user.DisplayName = entry.DirectoryAttributes["cn"].GetValue<string>();

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
