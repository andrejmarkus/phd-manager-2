using Microsoft.AspNetCore.Identity;
using PhDManager.Models;

namespace PhDManager.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Thesis> SupervisorTheses { get; set; }
    }
}
