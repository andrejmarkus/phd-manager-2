using Microsoft.AspNetCore.Identity;
using PhDManager.Services.IRepositories;

namespace PhDManager.Services
{
    public class RoleInitializer(RoleManager<IdentityRole> roleManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitializeAsync()
        {
            string[] roles = { "Admin", "User", "Teacher", "Officer", "Vicedean", "Graduant", "Reviewer" };

            foreach (string role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
