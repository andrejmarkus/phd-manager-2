using PhDManager.Models.Roles;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class IndividualPlan : BaseModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime? DissertationApplicationDate { get; set; }
        public DateTime? DissertationSubmissionDate { get; set; }
        public DateTime? StudyStartDate { get; set; }
        public DateTime? StudyEndDate { get; set; }
        public string ThematicAreas { get; set; } = string.Empty;
        public string WrittenThesisTitle { get; set; } = string.Empty;
        public string WrittenThesisRequiredLiterature { get; set; } = string.Empty;
        public string WrittenThesisRecommendedLiterature { get; set; } = string.Empty;
        public string WrittenThesisRecommendedLectures { get; set; } = string.Empty;
        public string[] Tasks { get; set; } = [];
        public DateTime?[] TaskDeadlines { get; set; } = [];
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual Student? Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = [];
        public virtual List<IndividualPlanSubject> IndividualPlanSubjects { get; set; } = [];
    }
}
