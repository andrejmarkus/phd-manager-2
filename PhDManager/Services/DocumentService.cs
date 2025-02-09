using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using System.Text;

namespace PhDManager.Services
{
    public class DocumentService
    {
        public string NormalizeName(string name)
        {
            return new string(name.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).Select(c => c == ' ' ? '_' : c).ToArray());
        }

        public Stream GenerateDocumentData(string path, Dictionary<string, string> replacements)
        {
            var documentStream = new MemoryStream();

            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(path))
            {
                var body = document.MainDocumentPart?.Document.Body;
                if (body is null) return documentStream;

                foreach (var replacement in replacements)
                {
                    var lines = replacement.Value.Split('\n');
                    foreach (var text in body.Descendants<Text>().ToList())
                    {
                        if (text.Text.Contains(replacement.Key))
                        {
                            var run = new Run();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                run.AppendChild(new Text(lines[i]));
                                if (i < lines.Length - 1) run.AppendChild(new Break());
                            }
                            text.Parent.AppendChild(run);
                            text.Remove();
                        }
                    }
                }

                document.Clone(documentStream);
            }
            
            documentStream.Seek(0, SeekOrigin.Begin);
            return documentStream;
        }
    }
}
