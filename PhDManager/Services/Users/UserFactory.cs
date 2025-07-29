using LdapForNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PhDManager.Data;
using System.Security.Claims;

namespace PhDManager.Services.Users;

public class UserFactory(IUserStore<ApplicationUser> userStore) : IUserFactory
{
    private readonly IUserEmailStore<ApplicationUser> _emailStore = GetEmailStore(userStore);

    public async Task<ApplicationUser> CreateUserAsync(LdapEntry entry)
    {
        var user = CreateUser();
        var mail = entry.DirectoryAttributes["mail"].GetValue<string>();
        var username = entry.DirectoryAttributes["uid"].GetValue<string>();

        await userStore.SetUserNameAsync(user, username, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, mail, CancellationToken.None);
        user.EmailConfirmed = true;
        user.DisplayName = entry.DirectoryAttributes["displayName"].GetValue<string>();

        return user;
    }

    public async Task<ApplicationUser> CreateUserAsync(string email, string username, string displayName)
    {
        var user = CreateUser();

        await userStore.SetUserNameAsync(user, username, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
        user.IsExternal = true;
        user.DisplayName = displayName;

        return user;
    }

    public async Task<ApplicationUser> CreateUserAsync(string email, ExternalLoginInfo externalLoginInfo)
    {
        var user = CreateUser();

        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
        user.IsExternal = true;
        user.DisplayName = externalLoginInfo.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        return user;
    }

    private static ApplicationUser CreateUser() =>
        Activator.CreateInstance<ApplicationUser>()
        ?? throw new InvalidOperationException($"Can't create '{nameof(ApplicationUser)}'.");

    private static IUserEmailStore<ApplicationUser> GetEmailStore(IUserStore<ApplicationUser> userStore)
    {
        return (IUserEmailStore<ApplicationUser>)userStore;
    }
}