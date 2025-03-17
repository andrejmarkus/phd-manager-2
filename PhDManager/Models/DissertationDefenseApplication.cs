using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class DissertationDefenseApplication : BaseModel
    {
        public virtual Student Student { get; set; } = default!;
        public string Nationality { get; set; } = string.Empty;
        public string Ethnicity { get; set; } = string.Empty;
        public string AchievedHigherEducation { get; set; } = string.Empty;
        public int ApplicationSubmissionYear { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
    }
}
