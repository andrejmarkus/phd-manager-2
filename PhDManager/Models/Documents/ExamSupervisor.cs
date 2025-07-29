using Microsoft.JSInterop;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class ExamSupervisor : BaseModel, IDocumentable
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public string OpponentDisplayName { get; set; } = string.Empty;
        public string OpponentMail { get; set; } = string.Empty;
        public string OpponentPhoneNumber { get; set; } = string.Empty;
        public string OpponentDepartment { get; set; } = string.Empty;

        public virtual Student Student { get; set; } = null!;

        public virtual Teacher Examiner { get; set; } = null!;
        public virtual Teacher Chairperson { get; set; } = null!;
        public virtual Teacher ExternalMember { get; set; } = null!;
        public virtual Teacher AcademicCommitteeMember { get; set; } = null!;
        public virtual Teacher AdditionalMember { get; set; } = null!;

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new ExamSupervisorDocument(jsRuntime, enumService, this);
        }
    }
}
