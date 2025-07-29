using LdapForNet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;
using PhDManager.Services.IRepositories;
using System.Security.Claims;
using PhDManager.Services.UserRoles;
using PhDManager.Services.Users;

namespace PhDManager.Services
{
    public class UsersService(
        IUnitOfWork unitOfWork,
        IUserStore<ApplicationUser> userStore,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AuthenticationStateProvider authenticationStateProvider,
        ActiveDirectoryService activeDirectoryService
        )
    {
        private readonly UserRoleStrategyFactory _roleStrategyFactory = new(userManager);
        private readonly UserFactory _userFactory = new(userStore);

        public IEnumerable<IdentityRole> Roles => roleManager.Roles;

        public IEnumerable<ApplicationUser> GetAll()
        {
            return userManager.Users;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return await userManager.GetUserAsync(authState.User);
        }

        public ApplicationUser? GetUserByUsernameAsync(string username)
        {
            return userManager.Users.FirstOrDefault(u => u.UserName == username);
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return (await userManager.GetUserAsync(authState.User))?.Id;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<string?> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles[0];
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            var currentUser = await GetCurrentUserAsync();
            if (currentUser is null) return null;
            return await GetUserRoleAsync(currentUser);
        }

        public async Task UpdateUsers()
        {
            var users = userManager.Users.Where(u => !u.IsExternal).ToList();
            foreach (var user in users)
            {
                await UpdateLdapUserData(user);
            }
        }

        public async Task DeleteUserAsync(ApplicationUser user)
        {
            var unusedUser = await userManager.FindByIdAsync(user.Id);

            switch (unusedUser)
            {
                case null:
                    return;
                case { IsExternal: true, Email: not null }:
                {
                    var invitation = await unitOfWork.Invitations.GetByEmailAsync(unusedUser.Email);
                    if (invitation is not null)
                    {
                        unitOfWork.Invitations.Delete(invitation);
                        await unitOfWork.CompleteAsync();
                    }

                    break;
                }
            }

            await userManager.DeleteAsync(unusedUser);
        }

        public async Task<ApplicationUser?> RegisterLdapUserWithoutPasswordAsync(LdapEntry entry, string role)
        {
            var user = await _userFactory.CreateUserAsync(entry);
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded) return null;

            await AssignRole(user, role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterLdapUserAsync(LdapEntry entry, string password, string role)
        {
            var user = await _userFactory.CreateUserAsync(entry);
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await AssignRole(user, role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterUserAsync(string email, string username, string displayName, string password)
        {
            var invitation = await unitOfWork.Invitations.GetByEmailAsync(email);
            if (invitation is null) return null;

            var user = await _userFactory.CreateUserAsync(email, username, displayName);
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return null;

            await AssignRole(user, invitation.Role);

            return user;
        }

        public async Task<ApplicationUser?> RegisterUserAsync(string email, ExternalLoginInfo externalLoginInfo)
        {
            var invitation = await unitOfWork.Invitations.GetByEmailAsync(email);
            if (invitation is null) return null;

            var user = await _userFactory.CreateUserAsync(email, externalLoginInfo);
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded) return null;

            await AssignRole(user, invitation.Role);

            return user;
        }

        private async Task AssignRole(ApplicationUser user, string role)
        {
            await userManager.AddToRoleAsync(user, role);
            var strategy = _roleStrategyFactory.GetStrategy(role);
            await strategy.ApplyRole(user);
            await unitOfWork.CompleteAsync();
        }

        private async Task UpdateLdapUserData(ApplicationUser user)
        {
            if (user.UserName is null) return;

            var entry = await activeDirectoryService.SearchUserAsync(user.UserName);
            if (entry is null) return;

            user.Email = entry.DirectoryAttributes["mail"].GetValue<string>();
            user.UserName = entry.DirectoryAttributes["uid"].GetValue<string>();
            user.DisplayName = entry.DirectoryAttributes["displayName"].GetValue<string>();

            await userManager.UpdateAsync(user);
        }
    }
}
