using Microsoft.AspNetCore.Identity;
using PhDManager.Models.Roles;

namespace PhDManager.Services
{
    public class RoleInitializer(RoleManager<IdentityRole> roleManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitializeAsync()
        {
            string[] roles = [Admin.Role, ExternalTeacher.Role, Student.Role, Teacher.Role];

            var rolesToCreate = roles.Where(role => _roleManager.FindByNameAsync(role).Result is null);

            foreach (string role in rolesToCreate)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
