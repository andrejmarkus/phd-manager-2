using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;

namespace PhDManager.Services.UserRoles;

public class AdminRoleStrategy(UserManager<ApplicationUser> userManager) : IUserRoleStrategy
{
    public async Task ApplyRole(ApplicationUser user)
    {
        var unusedUser = await userManager.FindByIdAsync(user.Id);
        if (unusedUser is null) return;

        unusedUser.Admin = new Admin();
        await userManager.UpdateAsync(unusedUser);
    }
}