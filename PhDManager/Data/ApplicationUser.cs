using Microsoft.AspNetCore.Identity;
using PhDManager.Models;
using System.ComponentModel.DataAnnotations;

namespace PhDManager.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Student
        public string? DisplayName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        public virtual Address Address { get; set; }
        public virtual StudyProgram? StudyProgram { get; set; }

        // Supervisor
        public virtual List<Thesis> SupervisorTheses { get; set; }

        // Reviewer
        public virtual List<Comment> Comments { get; set; }
    }
}
