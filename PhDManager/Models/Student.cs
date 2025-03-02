using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models
{
    public class Student : BaseModel
    {
        [NotMapped]
        public const string Role = "Študent";

        public string StartSchoolYear { get; set; } = string.Empty;
        public string EndSchoolYear { get; set; } = string.Empty;
        public bool IsExternal { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public virtual Address? Address { get; set; }
        public virtual StudyProgram? StudyProgram { get; set; }
        public virtual Thesis? ThesisInterest { get; set; }
        public virtual Thesis? Thesis { get; set; }
        public virtual IndividualPlan? IndividualPlan { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;

        [NotMapped]
        public bool HasInfo => User.Birthdate is not null && Address is not null && StudyProgram is not null;
    }
}
