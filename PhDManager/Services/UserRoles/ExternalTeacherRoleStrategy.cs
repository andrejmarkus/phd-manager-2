using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;

namespace PhDManager.Services.UserRoles;

public class ExternalTeacherRoleStrategy(UserManager<ApplicationUser> userManager) : IUserRoleStrategy
{
    public async Task ApplyRole(ApplicationUser user)
    {
        var unusedUser = await userManager.FindByIdAsync(user.Id);
        if (unusedUser is null) return;

        unusedUser.Teacher = new ExternalTeacher();
        await userManager.UpdateAsync(unusedUser);
    }
}