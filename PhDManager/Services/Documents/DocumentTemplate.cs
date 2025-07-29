using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text;

namespace PhDManager.Services.Documents;

public abstract class DocumentTemplate(IJSRuntime jsRuntime, EnumService enumService)
{
    protected abstract string DocumentName { get; }
    protected abstract string TemplatePath { get; }
    private string NormalizedDocumentName => NormalizeName(DocumentName) + ".docx";

    public async Task GenerateAndDownloadDocument()
    {
        var document = GenerateDocumentData(TemplatePath, GetReplacements());
        using var documentStream = new DotNetStreamReference(document);

        await jsRuntime.InvokeVoidAsync("saveAsFile", NormalizedDocumentName, documentStream);
    }

    protected abstract Dictionary<string, List<string?>> GetReplacements();

    protected static string NormalizeName(string name)
    {
        return new string([.. name.Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .Select(c => c == ' ' ? '_' : c)]);
    }

    protected static MemoryStream GenerateDocumentData(string path, Dictionary<string, List<string?>> replacements)
    {
        var documentStream = new MemoryStream();

        using (var document = WordprocessingDocument.CreateFromTemplate(path))
        {
            var body = document.MainDocumentPart?.Document.Body;
            if (body is null) return documentStream;

            foreach (var replacement in replacements)
            {
                var lines = replacement.Value?.Select(v => v?.Split('\n')).ToArray();
                var texts = body.Descendants<Text>().ToList();
                foreach (var text in texts)
                {
                    if (!text.Text.Contains(replacement.Key)) continue;

                    var runProperties = text.GetFirstChild<Run>()?.RunProperties;
                    if (lines is not null)
                    {
                        for (var i = 0; i < lines?.Length; i++)
                        {
                            if (lines[i] is null) continue;
                            var run = new Run
                            {
                                RunProperties = runProperties
                            };
                            for (var j = 0; j < lines[i]?.Length; j++)
                            {
                                if (lines[i]?[j] is null) continue;

                                run.AppendChild(new Text(lines[i]![j]));
                                if (j < lines[i]?.Length - 1) run.AppendChild(new Break());
                            }
                            if (i < lines?.Length - 1) run.AppendChild(new Break());
                            text.Parent?.AppendChild(run);
                        }
                    }
                    text.Remove();
                }
            }

            document.Clone(documentStream);
        }

        documentStream.Seek(0, SeekOrigin.Begin);
        return documentStream;
    }
}