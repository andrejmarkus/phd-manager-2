using Microsoft.JSInterop;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class DissertationDefenseApplication : BaseModel, IDocumentable
    {
        public virtual Student Student { get; set; } = default!;
        public string Nationality { get; set; } = string.Empty;
        public string Ethnicity { get; set; } = string.Empty;
        public string AchievedHigherEducation { get; set; } = string.Empty;
        public int ApplicationSubmissionYear { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new DissertationDefenseApplicationDocument(jsRuntime, enumService, this);
        }
    }
}
