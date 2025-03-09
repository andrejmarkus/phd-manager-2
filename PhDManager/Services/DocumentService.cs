using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.JSInterop;
using PhDManager.Components.Pages;
using PhDManager.Models;
using System.Globalization;
using System.Text;

namespace PhDManager.Services
{
    public class DocumentService(IJSRuntime JSRuntime, EnumService EnumService)
    {
        public async Task DownloadThesisDocument(Thesis thesis)
        {
            var documentName = NormalizeName(thesis.Title) + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "thesis_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Title}", new() { thesis.Title } },
                {"{Supervisor}", new() { thesis.Supervisor.User.DisplayName } },
                {"{SupervisorSpecialist}", new() { thesis.SupervisorSpecialist?.User.DisplayName } },
                {"{StudyProgram}", new() { thesis.StudyProgram.Name }},
                {"{StudyField}", new() { thesis.StudyProgram.StudyFieldName } },
                {"{DailyStudy}", new() { thesis.DailyStudy? "☑" : "☐" } },
                {"{ExternalStudy}", new() { thesis.ExternalStudy ? "☑" : "☐" } },
                {"{Subjects}", thesis.StudyProgram.ThesisSubjects.ToList<string?>() },
                {"{Description}", new() { thesis.Description }},
                {"{ScientificContribution}", new() { thesis.ScientificContribution }},
                {"{ScientificProgress}", new() { thesis.ScientificProgress }},
                {"{ResearchType}", new() { thesis.ResearchType }},
                {"{ResearchTask}", new() { thesis.ResearchTask }},
                {"{SolutionResults}", new() { thesis.SolutionResults }}
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadIndividualPlanDocument(IndividualPlan individualPlan)
        {
            if (individualPlan.Student is null) return;

            var documentName = NormalizeName(individualPlan.Student.User.DisplayName?? "") + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "individual_plan_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Student}", new() { individualPlan.Student.User.DisplayName } },
                {"{Birthdate}", new() { individualPlan.Student.User.Birthdate?.ToString("dd.MM.yyyy") }},
                {"{FullAddress}", new() { individualPlan.Student.Address?.FullAddress } },
                {"{PhoneNumber}", new() { individualPlan.Student.User.PhoneNumber }},
                {"{StudyForm}", new() { EnumService.GetLocalizedEnumValue(individualPlan.Student.StudyForm) }},
                {"{StudyProgram}", new() { individualPlan.Student.Thesis?.StudyProgram.Name }},
                {"{StudyField}", new() { individualPlan.Student.Thesis?.StudyProgram.StudyFieldName }},
                {"{Supervisor}", new() { individualPlan.Student.Thesis?.Supervisor.User.DisplayName }},
                {"{StudyStartDate}", new() { individualPlan.StudyStartDate?.ToString("dd.MM.yyyy") }},
                {"{DissertationExamDate}", new() { individualPlan.DissertationExamDate ?.ToString("dd.MM.yyyy") }},
                {"{DissertationSubmissionDate}", new() { individualPlan.DissertationSubmissionDate ?.ToString("MMMM yyyy") }},
                {"{StudyEndDate}", new() { individualPlan.StudyEndDate?.ToString("dd.MM.yyyy") }},
                {"{Subjects}", individualPlan.Subjects.Where(s => s.IsRequired).Select(s => s.Name).ToList<string?>() },
                {"{OptionalSubjects}", individualPlan.Subjects.Where(s => !s.IsRequired).Select(s => s.Name).ToList<string?>() },
                {"{WrittenThesisTitle}", new() { individualPlan.WrittenThesisTitle } },
                {"{WrittenThesisRequiredLiterature}", new() { individualPlan.WrittenThesisRequiredLiterature } },
                {"{WrittenThesisRecommendedLiterature}", new() { individualPlan.WrittenThesisRecommendedLiterature } },
                {"{WrittenThesisRecommendedLectures}", new() { individualPlan.WrittenThesisRecommendedLectures } },
                {"{ThesisTitle}", new() { individualPlan.Student.Thesis?.Title } },
                {"{ThesisReasearchTask}", new() { individualPlan.Student.Thesis?.ResearchTask } },
                {"{ThesisSolutionResults}", new() { individualPlan.Student.Thesis?.SolutionResults } },
                {"{Tasks}", individualPlan.Tasks.ToList<string?>() },
                {"{TaskDeadlines}", individualPlan.TaskDeadlines.Select(d => (d ?? DateTime.Now).ToString("MMMM yyyy")).ToList<string?>() },
                {"{CurrentDate}", new() { DateTime.Today.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadSubjectsExamApplicationDocument(SubjectsExamApplication subjectsExamApplication)
        {
            if (subjectsExamApplication.Student is null) return;

            var documentName = NormalizeName(subjectsExamApplication.Student.User.DisplayName ?? "") + "_predmety_prihlaska" + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "subjects_exam_application_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Student}", new() { subjectsExamApplication.Student.User.DisplayName } },
                {"{StudyForm}", new() { EnumService.GetLocalizedEnumValue(subjectsExamApplication.Student.StudyForm) } },
                {"{StudyProgram}", new() { subjectsExamApplication.Student.StudyProgram?.Name } },
                {"{StudyField}", new() { subjectsExamApplication.Student.StudyProgram?.StudyFieldName } },
                {"{Department}", new() { subjectsExamApplication.Student.Department?.Code } },
                {"{Supervisor}", new() { subjectsExamApplication.Student.Thesis?.Supervisor.User.DisplayName } },
                {"{StudyStartDate}", new() { subjectsExamApplication.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy") } },
                {"{Subjects}", subjectsExamApplication.Subjects.Select(s => s.Name).ToList<string?>() },
                {"{CurrentDate}", new() { DateTime.Today.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadExamApplicationDocument(ExamApplication examApplication)
        {
            if (examApplication.Student is null) return;

            var documentName = NormalizeName(examApplication.Student.User.DisplayName ?? "") + "_prihlaska" + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "exam_application_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Student}", new() { examApplication.Student.User.DisplayName } },
                {"{StudyForm}", new() { EnumService.GetLocalizedEnumValue(examApplication.Student.StudyForm) } },
                {"{StudyProgram}", new() { examApplication.Student.StudyProgram?.Name } },
                {"{StudyField}", new() { examApplication.Student.StudyProgram?.StudyFieldName } },
                {"{Department}", new() { examApplication.Student.Department?.Code } },
                {"{Supervisor}", new() { examApplication.Student.Thesis?.Supervisor.User.DisplayName } },
                {"{StudyStartDate}", new() { examApplication.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy") } },
                {"{WrittenThesisTitle}", new() { examApplication.WrittenThesisTitle } },
                {"{WrittenThesisTitleEnglish}", new() { examApplication.WrittenThesisTitleEnglish } },
                {"{Subjects}", examApplication.Subjects.Select(s => s.Name).ToList<string?>() },
                {"{CurrentDate}", new() { DateTime.Today.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        private string NormalizeName(string name)
        {
            return new string(name.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).Select(c => c == ' ' ? '_' : c).ToArray());
        }

        private Stream GenerateDocumentData(string path, Dictionary<string, List<string?>> replacements)
        {
            var documentStream = new MemoryStream();

            using (WordprocessingDocument document = WordprocessingDocument.CreateFromTemplate(path))
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

                        if (lines is not null)
                        {
                            for (int i = 0; i < lines?.Length; i++)
                            {
                                if (lines[i] is null) continue;
                                var run = new Run();
                                for (int j = 0; j < lines[i]?.Length; j++)
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
}