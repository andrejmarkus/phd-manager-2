using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;

namespace PhDManager.Services.UserRoles;

public class StudentRoleStrategy(UserManager<ApplicationUser> userManager) : IUserRoleStrategy
{
    public async Task ApplyRole(ApplicationUser user)
    {
        var unusedUser = await userManager.FindByIdAsync(user.Id);
        if (unusedUser is null) return;

        unusedUser.Students.Add(new Student());
        await userManager.UpdateAsync(unusedUser);
    }
}