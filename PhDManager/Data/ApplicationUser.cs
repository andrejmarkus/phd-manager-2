using Microsoft.AspNetCore.Identity;
using PhDManager.Models;

namespace PhDManager.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }

        public virtual List<Thesis> SupervisorTheses { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
