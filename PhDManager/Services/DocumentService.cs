using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.JSInterop;
using PhDManager.Models;
using System.Globalization;
using System.Text;

namespace PhDManager.Services
{
    public class DocumentService(IJSRuntime JSRuntime)
    {
        public async Task DownloadThesisDocument(Thesis thesis)
        {
            var documentName = NormalizeName(thesis.Title) + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "thesis_template.docx");
            var replacements = new Dictionary<string, string>()
            {
                {"{Title}", thesis.Title},
                {"{Supervisor}", thesis.Supervisor.DisplayName},
                {"{StudyProgram}", thesis.StudyProgram.Name},
                {"{StudyField}", thesis.StudyProgram.StudyFieldName},
                {"{DailyStudy}", thesis.DailyStudy ? "☑" : "☐"},
                {"{ExternalStudy}", thesis.ExternalStudy ? "☑" : "☐"},
                {"{Subject1}", thesis.SubjectNames[0]},
                {"{Subject2}", thesis.SubjectNames[1]},
                {"{Subject3}", thesis.SubjectNames[2]},
                {"{Description}", thesis.Description},
                {"{ScientificContribution}", thesis.ScientificContribution},
                {"{ScientificProgress}", thesis.ScientificProgress},
                {"{ResearchType}", thesis.ResearchType},
                {"{ResearchTask}", thesis.ResearchTask},
                {"{SolutionResults}", thesis.SolutionResults}
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadIndividualPlanDocument(IndividualPlan individualPlan)
        {
            var documentName = NormalizeName(individualPlan.User.DisplayName) + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "individual_plan_template.docx");
            var replacements = new Dictionary<string, string>()
            {
                {"{Student}", individualPlan.User.DisplayName},
                {"{Birthdate}", individualPlan.User.Birthdate?.ToString("dd.MM.yyyy")},
                {"{FullAddress}", individualPlan.User.Address.FullAddress },
                {"{PhoneNumber}", individualPlan.User.PhoneNumber},
                {"{StudyForm}", individualPlan.User.IsExternal ? "Externá" : "Denná"},
                {"{StudyProgram}", individualPlan.Thesis.StudyProgram.Name},
                {"{StudyField}", individualPlan.Thesis.StudyProgram.StudyFieldName},
                {"{Supervisor}", individualPlan.Thesis.Supervisor.DisplayName},
                {"{StudyStartDate}", individualPlan.StudyStartDate?.ToString("dd.MM.yyyy")},
                {"{DissertationExamDate}", individualPlan.DissertationExamDate?.ToString("dd.MM.yyyy")},
                {"{DissertationSubmissionDate}", individualPlan.DissertationSubmissionDate?.ToString("MMMM yyyy") },
                {"{StudyEndDate}", individualPlan.StudyEndDate?.ToString("dd.MM.yyyy")},
                {"{Subject1}", individualPlan.Thesis.SubjectNames[0]},
                {"{Subject2}", individualPlan.Thesis.SubjectNames[1]},
                {"{Subject3}", individualPlan.Thesis.SubjectNames[2]},
                {"{ThesisTitle}", individualPlan.Thesis.Title},
                {"{CurrentDate}", DateTime.Today.ToString("dd.MM.yyyy")},
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        private string NormalizeName(string name)
        {
            return new string(name.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).Select(c => c == ' ' ? '_' : c).ToArray());
        }

        private Stream GenerateDocumentData(string path, Dictionary<string, string> replacements)
        {
            var documentStream = new MemoryStream();

            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(path))
            {
                var body = document.MainDocumentPart?.Document.Body;
                if (body is null) return documentStream;

                foreach (var replacement in replacements)
                {
                    var lines = replacement.Value.Split('\n');
                    var texts = body.Descendants<Text>().ToList();
                    foreach (var text in texts)
                    {
                        if (!text.Text.Contains(replacement.Key)) continue;

                        var run = new Run();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            run.AppendChild(new Text(lines[i]));
                            if (i < lines.Length - 1) run.AppendChild(new Break());
                        }
                        text.Parent?.AppendChild(run);
                        text.Remove();
                    }
                }

                document.Clone(documentStream);
            }

            documentStream.Seek(0, SeekOrigin.Begin);
            return documentStream;
        }
    }
}