using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class IndividualPlan : BaseModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime? DissertationExamDate { get; set; }
        public DateTime? DissertationSubmissionDate { get; set; }
        public DateTime? StudyStartDate { get; set; }
        public DateTime? StudyEndDate { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = [];
    }
}
