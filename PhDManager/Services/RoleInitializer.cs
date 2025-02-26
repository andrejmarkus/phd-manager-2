using Microsoft.AspNetCore.Identity;
using PhDManager.Models;

namespace PhDManager.Services
{
    public class RoleInitializer(RoleManager<IdentityRole> roleManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitializeAsync()
        {
            string[] roles = { Admin.Role, External.Role, Student.Role, Teacher.Role };

            foreach (string role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) is null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
