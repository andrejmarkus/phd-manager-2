using Microsoft.JSInterop;
using PhDManager.Models.Enums;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class StudentEvaluation : BaseModel, IDocumentable
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
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public Conclusion Conclusion { get; set; }
        public virtual Student? Student { get; set; }

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new StudentEvaluationDocument(jsRuntime, enumService, this);
        }
    }
}
