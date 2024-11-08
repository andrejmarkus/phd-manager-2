using LdapForNet;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;

namespace PhDManager.Services
{
    public class UsersService(IUserStore<ApplicationUser> UserStore, UserManager<ApplicationUser> UserManager)
    {
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
