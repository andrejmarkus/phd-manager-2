using Microsoft.JSInterop;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class DissertationDefenseSupervisor : BaseModel, IDocumentable
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public int CreditsCount { get; set; }
        public int ApplicationYear { get; set; }
        public string[] OpponentDisplayNames { get; set; } = [];
        public string[] OpponentWorkplaceAddresses { get; set; } = [];
        public string[] OpponentPhoneNumbers { get; set; } = [];
        public string[] OpponentEmails { get; set; } = [];
        public virtual Student Student { get; set; } = null!;

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new DissertationDefenseSupervisorDocument(jsRuntime, enumService, this);
        }
    }
}
