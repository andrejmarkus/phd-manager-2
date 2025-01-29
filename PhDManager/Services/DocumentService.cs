using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using System.Text;

namespace PhDManager.Services
{
    public class DocumentService
    {
        public MemoryStream GenerateDocument(string path, Dictionary<string, string> replacements)
        {
            var memoryStream = new MemoryStream();

            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(path))
            {
                var body = document.MainDocumentPart?.Document.Body;
                if (body is null) return memoryStream;

                foreach (var replacement in replacements)
                {
                    foreach (var text in body.Descendants<Text>())
                    {
                        if (text.Text.Contains(replacement.Key))
                        {
                            text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                        }
                    }
                }

                document.Clone(memoryStream);
            }
            
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public string NormalizeName(string name)
        {
            return new string(name.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).Select(c => c == ' ' ? '_' : c).ToArray());
        }
    }
}
