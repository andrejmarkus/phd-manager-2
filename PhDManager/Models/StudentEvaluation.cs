using PhDManager.Models.Enums;
using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class StudentEvaluation : BaseModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string SchoolYear { get; set; } = string.Empty;
        public string ThesisState { get; set; } = string.Empty;
        public string CreditsEvaluation { get; set; } = string.Empty;
        public string ScientificEvaluation { get; set; } = string.Empty;
        public string AssignmentsState { get; set; } = string.Empty;
        public string ModificationProposal { get; set; } = string.Empty;
        public string AdditionalEvaluation { get; set; } = string.Empty;
        public DateTime? PlannedDissertationApplicationDate { get; set; }
        public DateTime? PlannedDissertationSubmissionDate { get; set; }
        public DateTime? PlannedDissertationExamDate { get; set; }
        public Conclusion Conclusion { get; set; }
        public virtual Student? Student { get; set; }
    }
}
