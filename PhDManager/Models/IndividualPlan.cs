using PhDManager.Data;

namespace PhDManager.Models
{
    public class IndividualPlan
    {
        public int Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime? DissertationExamDate { get; set; }
        public DateTime? DissertationSubmissionDate { get; set; }
        public DateTime? StudyEndDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Thesis Thesis { get; set; }
    }
}
