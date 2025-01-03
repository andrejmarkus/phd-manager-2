using PhDManager.Data;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class IndividualPlan
    {
        public int Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime? DissertationExamDate { get; set; }
        public DateTime? DissertationSubmissionDate { get; set; }
        public DateTime? StudyEndDate { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }
        [JsonIgnore]
        public virtual Thesis Thesis { get; set; }
        public virtual List<Subject> Subjects { get; set; } = new();
    }
}
