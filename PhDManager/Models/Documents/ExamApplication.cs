using Microsoft.JSInterop;
using PhDManager.Models.Roles;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents
{
    public class ExamApplication : BaseModel, IDocumentable
    {
        public string WrittenThesisTitle { get; set; } = string.Empty;
        public string WrittenThesisTitleEnglish { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public virtual Student? Student { get; set; } = null!;
        public virtual List<Subject> Subjects { get; set; } = null!;

        public DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService)
        {
            return new ExamApplicationDocument(jsRuntime, enumService, this);
        }
    }
}
