using Microsoft.AspNetCore.Identity;
using PhDManager.Models.Roles;

namespace PhDManager.Services
{
    public class RoleInitializer(RoleManager<IdentityRole> roleManager)
    {

        public async Task InitializeAsync()
        {
            string[] roles = [Admin.Role, ExternalTeacher.Role, Student.Role, Teacher.Role];

            var rolesToCreate = roles.Where(role => roleManager.FindByNameAsync(role).Result is null);

            foreach (var role in rolesToCreate)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
