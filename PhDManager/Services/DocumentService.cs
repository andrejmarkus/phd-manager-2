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
            var replacements = new Dictionary<string, string?>()
            {
                {"{Title}", thesis.Title},
                {"{Supervisor}", thesis.Supervisor.User.DisplayName},
                {"{SupervisorSpecialist}", thesis.SupervisorSpecialist?.User.DisplayName},
                {"{StudyProgram}", thesis.StudyProgram.Name},
                {"{StudyField}", thesis.StudyProgram.StudyFieldName},
                {"{DailyStudy}", thesis.DailyStudy ? "☑" : "☐"},
                {"{ExternalStudy}", thesis.ExternalStudy ? "☑" : "☐"},
                {"{Subject1}", thesis.StudyProgram.ThesisSubjects[0]},
                {"{Subject2}", thesis.StudyProgram.ThesisSubjects[1]},
                {"{Subject3}", thesis.StudyProgram.ThesisSubjects[2]},
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
            var documentName = NormalizeName(individualPlan.Student.User.DisplayName?? "") + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "individual_plan_template.docx");
            var replacements = new Dictionary<string, string?>()
            {
                {"{Student}", individualPlan.Student.User.DisplayName},
                {"{Birthdate}", individualPlan.Student.User.Birthdate?.ToString("dd.MM.yyyy")},
                {"{FullAddress}", individualPlan.Student.Address?.FullAddress },
                {"{PhoneNumber}", individualPlan.Student.User.PhoneNumber},
                {"{StudyForm}", individualPlan.Student.IsExternal ? "Externá" : "Denná"},
                {"{StudyProgram}", individualPlan.Student.Thesis?.StudyProgram.Name},
                {"{StudyField}", individualPlan.Student.Thesis?.StudyProgram.StudyFieldName},
                {"{Supervisor}", individualPlan.Student.Thesis?.Supervisor.User.DisplayName},
                {"{StudyStartDate}", individualPlan.StudyStartDate?.ToString("dd.MM.yyyy")},
                {"{DissertationExamDate}", individualPlan.DissertationExamDate?.ToString("dd.MM.yyyy")},
                {"{DissertationSubmissionDate}", individualPlan.DissertationSubmissionDate?.ToString("MMMM yyyy")},
                {"{StudyEndDate}", individualPlan.StudyEndDate?.ToString("dd.MM.yyyy")},
                {"{Subject1}", individualPlan.Subjects[0].Name},
                {"{Subject2}", individualPlan.Subjects[1].Name},
                {"{Subject3}", individualPlan.Subjects[2].Name},
                {"{OptionalSubject1}", individualPlan.OptionalSubjects.Count >= 1 ? individualPlan.OptionalSubjects[0].Name : null},
                {"{OptionalSubject2}", individualPlan.OptionalSubjects.Count >= 2 ? individualPlan.OptionalSubjects[1].Name : null},
                {"{WrittenThesisTitle}", individualPlan.WrittenThesisTitle},
                {"{WrittenThesisRequiredLiterature}", individualPlan.WrittenThesisRequiredLiterature},
                {"{WrittenThesisRecommendedLiterature}", individualPlan.WrittenThesisRecommendedLiterature},
                {"{WrittenThesisRecommendedLectures}", individualPlan.WrittenThesisRecommendedLectures},
                {"{ThesisTitle}", individualPlan.Student.Thesis?.Title},
                {"{ThesisReasearchTask}", individualPlan.Student.Thesis?.ResearchTask},
                {"{ThesisSolutionResults}", individualPlan.Student.Thesis?.SolutionResults},
                {"{Task1}", individualPlan.Tasks[0]},
                {"{Task2}", individualPlan.Tasks[1]},
                {"{Task3}", individualPlan.Tasks[2]},
                {"{Task4}", individualPlan.Tasks[3]},
                {"{Task5}", individualPlan.Tasks[4]},
                {"{Task6}", individualPlan.Tasks[5]},
                {"{TaskDeadline1}", individualPlan.TaskDeadlines[0]?.ToString("MMMM yyyy")},
                {"{TaskDeadline2}", individualPlan.TaskDeadlines[1]?.ToString("MMMM yyyy")},
                {"{TaskDeadline3}", individualPlan.TaskDeadlines[2]?.ToString("MMMM yyyy")},
                {"{TaskDeadline4}", individualPlan.TaskDeadlines[3]?.ToString("MMMM yyyy")},
                {"{TaskDeadline5}", individualPlan.TaskDeadlines[4]?.ToString("MMMM yyyy")},
                {"{TaskDeadline6}", individualPlan.TaskDeadlines[5]?.ToString("MMMM yyyy")},
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

        private Stream GenerateDocumentData(string path, Dictionary<string, string?> replacements)
        {
            var documentStream = new MemoryStream();

            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(path))
            {
                var body = document.MainDocumentPart?.Document.Body;
                if (body is null) return documentStream;

                foreach (var replacement in replacements)
                {
                    var lines = replacement.Value?.Split('\n');
                    var texts = body.Descendants<Text>().ToList();
                    foreach (var text in texts)
                    {
                        if (!text.Text.Contains(replacement.Key)) continue;

                        if (lines is not null)
                        {
                            var run = new Run();
                            for (int i = 0; i < lines.Length; i++)
                            {
                                run.AppendChild(new Text(lines[i]));
                                if (i < lines.Length - 1) run.AppendChild(new Break());
                            }
                            text.Parent?.AppendChild(run);
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
}