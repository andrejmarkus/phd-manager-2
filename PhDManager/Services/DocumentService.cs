using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.JSInterop;
using PhDManager.Components.Pages;
using PhDManager.Models;
using PhDManager.Models.Enums;
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
                {"{DissertationExamDate}", new() { individualPlan.DissertationApplicationDate ?.ToString("dd.MM.yyyy") }},
                {"{DissertationSubmissionDate}", new() { individualPlan.DissertationSubmissionDate ?.ToString("MMMM yyyy") }},
                {"{StudyEndDate}", new() { individualPlan.StudyEndDate?.ToString("dd.MM.yyyy") }},
                {"{Subjects}", individualPlan.Subjects.Where(s => s.RequirementLevel == RequirementLevel.Compulsory).Select(s => s.Name).ToList<string?>() },
                {"{OptionalSubjects}", individualPlan.Subjects.Where(s => s.RequirementLevel != RequirementLevel.Compulsory).Select(s => s.Name).ToList<string?>() },
                {"{WrittenThesisTitle}", new() { individualPlan.WrittenThesisTitle } },
                {"{WrittenThesisRequiredLiterature}", new() { individualPlan.WrittenThesisRequiredLiterature } },
                {"{WrittenThesisRecommendedLiterature}", new() { individualPlan.WrittenThesisRecommendedLiterature } },
                {"{WrittenThesisRecommendedLectures}", new() { individualPlan.WrittenThesisRecommendedLectures } },
                {"{ThesisTitle}", new() { individualPlan.Student.Thesis?.Title } },
                {"{ThesisReasearchTask}", new() { individualPlan.Student.Thesis?.ResearchTask } },
                {"{ThesisSolutionResults}", new() { individualPlan.Student.Thesis?.SolutionResults } },
                {"{Tasks}", individualPlan.Tasks.ToList<string?>() },
                {"{TaskDeadlines}", individualPlan.TaskDeadlines.Select(d => (d ?? DateTime.Now).ToString("MMMM yyyy")).ToList<string?>() },
                {"{CurrentDate}", new() { individualPlan.CurrentDate.ToString("dd.MM.yyyy") } }
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
                {"{CurrentDate}", new() { subjectsExamApplication.CurrentDate.ToString("dd.MM.yyyy") } }
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
                {"{CurrentDate}", new() { examApplication.CurrentDate.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadStudentEvaluationDocument(StudentEvaluation studentEvaluation)
        {
            if (studentEvaluation.Student is null) return;

            var documentName = NormalizeName(studentEvaluation.Student.User.DisplayName ?? "") + "_hodnotenie" + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "student_evaluation_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{SchoolYear}", new() { studentEvaluation.SchoolYear } },
                {"{Student}", new() { studentEvaluation.Student.User.DisplayName } },
                {"{StudyForm}", new() { EnumService.GetLocalizedEnumValue(studentEvaluation.Student.StudyForm) } },
                {"{StudyStartDate}", new() { studentEvaluation.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy") } },
                {"{StudyField}", new() { studentEvaluation.Student.StudyProgram?.StudyFieldName } },
                {"{StudyProgram}", new() { studentEvaluation.Student.StudyProgram?.Name } },
                {"{Supervisor}", new() { studentEvaluation.Student.Thesis?.Supervisor.User.DisplayName } },
                {"{Department}", new() { studentEvaluation.Student.Department?.Code } },
                {"{SubjectsAndGrades}", studentEvaluation.Student.IndividualPlan.Subjects.Zip(studentEvaluation.Student.IndividualPlan.IndividualPlanSubjects, (subject, grade) => $"{subject.Name}\t{EnumService.GetLocalizedEnumValue(grade.Grade)}").ToList<string?>() },
                {"{WrittenThesisTitle}", new() { studentEvaluation.Student.IndividualPlan.WrittenThesisTitle } },
                {"{PlannedDissertationSubmissionDate}", new() { studentEvaluation.PlannedDissertationSubmissionDate?.ToString("dd.MM.yyyy") } },
                {"{DissertationSubmissionDate}", new() { studentEvaluation.Student.IndividualPlan.DissertationSubmissionDate?.ToString("dd.MM.yyyy") } },
                {"{PlannedDissertationExamDate}", new() { studentEvaluation.PlannedDissertationExamDate?.ToString("dd.MM.yyyy") } },
                {"{DissertationExamDate}", new() { studentEvaluation.Student.DissertationExamDate?.ToString("dd.MM.yyyy") } },
                {"{DissertationExamResult}", new() { studentEvaluation.Student.DissertationExamResult } },
                {"{DissertationExamTranscript}", new() { studentEvaluation.Student.DissertationExamTranscript } },
                {"{ThesisTitle}", new() { studentEvaluation.Student.Thesis?.Title } },
                {"{ThesisState}", new() { studentEvaluation.ThesisState } },
                {"{PlannedDissertationApplicationDate}", new() { studentEvaluation.PlannedDissertationApplicationDate?.ToString("dd.MM.yyyy") } },
                {"{DissertationApplicationDate}", new() { studentEvaluation.Student.IndividualPlan.DissertationApplicationDate?.ToString("dd.MM.yyyy") } },
                {"{CreditsEvaluation}", new() { studentEvaluation.CreditsEvaluation } },
                {"{ScientificEvaluation}", new() { studentEvaluation.ScientificEvaluation } },
                {"{AssignmentsState}", new() { studentEvaluation.AssignmentsState } },
                {"{ModificationProposal}", new() { studentEvaluation.ModificationProposal } },
                {"{Conclusion}", new() { EnumService.GetLocalizedEnumValue(studentEvaluation.Conclusion) } },
                {"{AdditionalEvaluation}", new() { studentEvaluation.AdditionalEvaluation } },
                {"{CurrentDate}", new() { studentEvaluation.CurrentDate.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadExamSupervisorDocument(ExamSupervisor examSupervisor)
        {
            var documentName = NormalizeName(examSupervisor.Student.User.DisplayName ?? "") + "_ds_skolitel" + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "exam_supervisor_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Student}", new() { examSupervisor.Student.User.UserName } },
                {"{Supervisor}", new() { examSupervisor.Student.Thesis?.Supervisor.User.DisplayName } },
                {"{Opponent}", new() { examSupervisor.OpponentDisplayName} },
                {"{OpponentMail}", new() { examSupervisor.OpponentMail } },
                {"{OpponentPhoneNumber}", new() { examSupervisor.OpponentPhoneNumber } },
                {"{OpponentDepartment}", new() { examSupervisor.OpponentDepartment } },
                {"{Examiner}", new() { examSupervisor.Examiner.User.DisplayName } },
                {"{Chairperson}", new() { examSupervisor.Chairperson.User.DisplayName } },
                {"{ChairpersonDepartment}", new() { examSupervisor.Chairperson.Department?.Code } },
                {"{ChairpersonMail}", new() { examSupervisor.Chairperson.User.Email } },
                {"{ChairpersonPhoneNumber}", new() { examSupervisor.Chairperson.User.PhoneNumber } },
                {"{ExternalMember}", new() { examSupervisor.ExternalMember.User.DisplayName } },
                {"{ExternalMemberDepartment}", new() { examSupervisor.ExternalMember.Department?.Code } },
                {"{ExternalMemberMail}", new() { examSupervisor.ExternalMember.User.Email } },
                {"{ExternalMemberPhoneNumber}", new() { examSupervisor.ExternalMember.User.PhoneNumber } },
                {"{AcademicCommitteeMember}", new() { examSupervisor.AcademicCommitteeMember.User.DisplayName } },
                {"{AcademicCommitteeMemberDepartment}", new() { examSupervisor.AcademicCommitteeMember.Department?.Code } },
                {"{AcademicCommitteeMemberMail}", new() { examSupervisor.AcademicCommitteeMember.User.Email } },
                {"{AcademicCommitteeMemberPhoneNumber}", new() { examSupervisor.AcademicCommitteeMember.User.PhoneNumber } },
                {"{AdditionalMember}", new() { examSupervisor.AdditionalMember.User.DisplayName } },
                {"{AdditionalMemberDepartment}", new() { examSupervisor.AdditionalMember.Department?.Code } },
                {"{AdditionalMemberMail}", new() { examSupervisor.AdditionalMember.User.Email } },
                {"{AdditionalMemberPhoneNumber}", new() { examSupervisor.AdditionalMember.User.PhoneNumber } },
                {"{CurrentDate}", new() { examSupervisor.CurrentDate.ToString("dd.MM.yyyy") } }
            };

            var document = GenerateDocumentData(path, replacements);
            using var documentStream = new DotNetStreamReference(document);

            await JSRuntime.InvokeVoidAsync("saveAsFile", documentName, documentStream);
        }

        public async Task DownloadDissertationDefenseSupervisorDocument(DissertationDefenseSupervisor dissertationDefenseSupervisor)
        {
            var documentName = NormalizeName(dissertationDefenseSupervisor.Student.User.DisplayName ?? "") + "_dp_skolitel" + ".docx";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "exam_supervisor_template.docx");
            var replacements = new Dictionary<string, List<string?>>()
            {
                {"{Student}", new() { dissertationDefenseSupervisor.Student.User.UserName } },
                {"{StudyForm}", new() { EnumService.GetLocalizedEnumValue(dissertationDefenseSupervisor.Student.StudyForm) } },
                {"{StudyProgram}", new() { dissertationDefenseSupervisor.Student.StudyProgram?.Name } },
                {"{StudyField}", new() { dissertationDefenseSupervisor.Student.StudyProgram?.StudyFieldName } },
                {"{Department}", new() { dissertationDefenseSupervisor.Student.Department?.Code } },
                {"{Supervisor}", new() { dissertationDefenseSupervisor.Student.Thesis?.Supervisor.User.DisplayName } },
                {"{StudyStartDate}", new() { dissertationDefenseSupervisor.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy") } },
                {"{StudyEndDate}", new() { dissertationDefenseSupervisor.Student.IndividualPlan?.StudyEndDate?.ToString("dd.MM.yyyy") } },
                {"{CreditsCount}", new() { dissertationDefenseSupervisor.CreditsCount.ToString() } },
                {"{ApplicationYear}", new() { dissertationDefenseSupervisor.ApplicationYear.ToString() } },
                {"{ThesisTitle}", new() { dissertationDefenseSupervisor.Student.Thesis?.Title } },
                {"{Opponent1}", new() { dissertationDefenseSupervisor.OpponentDisplayNames[0] } },
                {"{Opponent1WorkplaceAddress}", new() { dissertationDefenseSupervisor.OpponentWorkplaceAddresses[0] } },
                {"{Opponent1PhoneNumber}", new() { dissertationDefenseSupervisor.OpponentPhoneNumbers[0] } },
                {"{Opponent1Mail}", new() { dissertationDefenseSupervisor.OpponentEmails[0] } },
                {"{Opponent2}", new() { dissertationDefenseSupervisor.OpponentDisplayNames[1] } },
                {"{Opponent2WorkplaceAddress}", new() { dissertationDefenseSupervisor.OpponentWorkplaceAddresses[1] } },
                {"{Opponent2PhoneNumber}", new() { dissertationDefenseSupervisor.OpponentPhoneNumbers[1] } },
                {"{Opponent2Mail}", new() { dissertationDefenseSupervisor.OpponentEmails[1] } },
                {"{Opponent3}", new() { dissertationDefenseSupervisor.OpponentDisplayNames[2] } },
                {"{Opponent3WorkplaceAddress}", new() { dissertationDefenseSupervisor.OpponentWorkplaceAddresses[2] } },
                {"{Opponent3PhoneNumber}", new() { dissertationDefenseSupervisor.OpponentPhoneNumbers[2] } },
                {"{Opponent3Mail}", new() { dissertationDefenseSupervisor.OpponentEmails[2] } },
                {"{CurrentDate}", new() { dissertationDefenseSupervisor.CurrentDate.ToString("dd.MM.yyyy") } }
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