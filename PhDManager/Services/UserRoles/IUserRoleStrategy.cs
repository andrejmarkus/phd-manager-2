using PhDManager.Data;

namespace PhDManager.Services.UserRoles;

public interface IUserRoleStrategy
{
    Task ApplyRole(ApplicationUser user);
}