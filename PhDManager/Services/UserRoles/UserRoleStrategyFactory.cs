using Microsoft.AspNetCore.Identity;
using PhDManager.Data;
using PhDManager.Models.Roles;

namespace PhDManager.Services.UserRoles;

public class UserRoleStrategyFactory(UserManager<ApplicationUser> userManager)
{
    private readonly Dictionary<string, IUserRoleStrategy> _strategies = new()
    {
        { Admin.Role, new AdminRoleStrategy(userManager) },
        { Student.Role, new StudentRoleStrategy(userManager) },
        { Teacher.Role, new TeacherRoleStrategy(userManager) },
        { ExternalTeacher.Role, new ExternalTeacherRoleStrategy(userManager) }
    };

    public IUserRoleStrategy GetStrategy(string role)
    {
        return _strategies.TryGetValue(role, out var strategy) ? strategy : throw new IndexOutOfRangeException();
    }
}