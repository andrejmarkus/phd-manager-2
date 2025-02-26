using Microsoft.AspNetCore.Identity;
using PhDManager.Models;

namespace PhDManager.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public DateTime? Birthdate { get; set; }

        public virtual Admin? Admin { get; set; }

        public virtual List<Student> Students { get; set; } = [];

        public virtual Teacher? Teacher { get; set; }

        public virtual External? External { get; set; }
    }
}
