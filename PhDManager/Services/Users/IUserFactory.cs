using LdapForNet;
using Microsoft.AspNetCore.Identity;
using PhDManager.Data;

namespace PhDManager.Services.Users;

public interface IUserFactory
{
    Task<ApplicationUser> CreateUserAsync(LdapEntry entry);
    Task<ApplicationUser> CreateUserAsync(string email, string username, string displayName);
    Task<ApplicationUser> CreateUserAsync(string email, ExternalLoginInfo externalLoginInfo);
}