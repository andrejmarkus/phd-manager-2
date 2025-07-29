using Microsoft.JSInterop;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class SubjectsExamApplication : BaseModel, IDocumentable
    {
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public virtual Student? Student { get; set; } = null!;
        public virtual List<Subject> Subjects { get; set; } = [];

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new SubjectsExamApplicationDocument(jsRuntime, enumService, this);
        }
    }
}
