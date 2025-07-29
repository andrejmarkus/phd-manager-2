using Microsoft.JSInterop;
using PhDManager.Services;
using PhDManager.Services.Documents;

namespace PhDManager.Models.Documents;

public interface IDocumentable
{
    DocumentTemplate CreateDocument(IJSRuntime jsRuntime, EnumService enumService);
}