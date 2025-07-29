using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services
{
    public class DocumentService(IJSRuntime jsRuntime, EnumService enumService)
    {
        public async Task Download<T>(T documentable) where T : IDocumentable
        {
            var document = documentable.CreateDocument(jsRuntime, enumService);
            await document.GenerateAndDownloadDocument();
        }
    }
}