using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class SubjectsExamApplicationDocument(IJSRuntime jsRuntime, EnumService enumService, SubjectsExamApplication subjectsExamApplication) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => subjectsExamApplication.Student?.User.DisplayName + "_predmety_prihlaska";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates",
        "subjects_exam_application_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            { "{Student}", [subjectsExamApplication.Student?.User.DisplayName] },
            { "{StudyForm}", [_enumService.GetLocalizedEnumValue(subjectsExamApplication.Student?.StudyForm)] },
            { "{StudyProgram}", [subjectsExamApplication.Student?.StudyProgram?.Name] },
            { "{StudyField}", [subjectsExamApplication.Student?.StudyProgram?.StudyFieldName] },
            { "{Department}", [subjectsExamApplication.Student?.Department?.Code] },
            { "{Supervisor}", [subjectsExamApplication.Student?.Thesis?.Supervisor.User.DisplayName] },
            { "{StudyStartDate}", [subjectsExamApplication.Student?.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy")] },
            { "{Subjects}", subjectsExamApplication.Subjects.Select(s => s.Name).ToList<string?>() },
            { "{CurrentDate}", [subjectsExamApplication.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}