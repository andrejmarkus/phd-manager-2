using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class StudentEvaluationDocument(IJSRuntime jsRuntime, EnumService enumService, StudentEvaluation studentEvaluation) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => studentEvaluation.Student?.User.DisplayName + "_hodnotenie";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "student_evaluation_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            { "{SchoolYear}", [studentEvaluation.SchoolYear] },
            { "{Student}", [studentEvaluation.Student?.User.DisplayName] },
            { "{StudyForm}", [_enumService.GetLocalizedEnumValue(studentEvaluation.Student?.StudyForm)] },
            { "{StudyStartDate}", [studentEvaluation.Student?.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy")] },
            { "{StudyField}", [studentEvaluation.Student?.StudyProgram?.StudyFieldName] },
            { "{StudyProgram}", [studentEvaluation.Student?.StudyProgram?.Name] },
            { "{Supervisor}", [studentEvaluation.Student?.Thesis?.Supervisor.User.DisplayName] },
            { "{Department}", [studentEvaluation.Student?.Department?.Code] },
            {
                "{SubjectsAndGrades}",
                studentEvaluation.Student?.IndividualPlan?.Subjects
                    .Zip(studentEvaluation.Student.IndividualPlan.IndividualPlanSubjects,
                        (subject, grade) => $"{subject.Name}\t{_enumService.GetLocalizedEnumValue(grade.Grade)}")
                    .ToList<string?>() ?? []
            },
            { "{WrittenThesisTitle}", [studentEvaluation.Student?.IndividualPlan?.WrittenThesisTitle] },
            {
                "{PlannedDissertationSubmissionDate}",
                [studentEvaluation.PlannedDissertationSubmissionDate?.ToString("dd.MM.yyyy")]
            },
            {
                "{DissertationSubmissionDate}",
                [studentEvaluation.Student?.IndividualPlan?.DissertationSubmissionDate?.ToString("dd.MM.yyyy")]
            },
            {
                "{PlannedDissertationExamDate}", [studentEvaluation.PlannedDissertationExamDate?.ToString("dd.MM.yyyy")]
            },
            { "{DissertationExamDate}", [studentEvaluation.Student?.DissertationExamDate?.ToString("dd.MM.yyyy")] },
            { "{DissertationExamResult}", [studentEvaluation.Student?.DissertationExamResult] },
            { "{DissertationExamTranscript}", [studentEvaluation.Student?.DissertationExamTranscript] },
            { "{ThesisTitle}", [studentEvaluation.Student?.Thesis?.Title] },
            { "{ThesisState}", [studentEvaluation.ThesisState] },
            {
                "{PlannedDissertationApplicationDate}",
                [studentEvaluation.PlannedDissertationApplicationDate?.ToString("dd.MM.yyyy")]
            },
            {
                "{DissertationApplicationDate}",
                [studentEvaluation.Student?.IndividualPlan?.DissertationApplicationDate?.ToString("dd.MM.yyyy")]
            },
            { "{CreditsEvaluation}", [studentEvaluation.CreditsEvaluation] },
            { "{ScientificEvaluation}", [studentEvaluation.ScientificEvaluation] },
            { "{AssignmentsState}", [studentEvaluation.AssignmentsState] },
            { "{ModificationProposal}", [studentEvaluation.ModificationProposal] },
            { "{Conclusion}", [_enumService.GetLocalizedEnumValue(studentEvaluation.Conclusion)] },
            { "{AdditionalEvaluation}", [studentEvaluation.AdditionalEvaluation] },
            { "{CurrentDate}", [studentEvaluation.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}