using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class ExamApplicationDocument(IJSRuntime jsRuntime, EnumService enumService, ExamApplication examApplication) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => examApplication.Student?.User.DisplayName + "_prihlaska";

    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates",
        "exam_application_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            {"{Student}", [examApplication.Student?.User.DisplayName] },
            {"{StudyForm}", [_enumService.GetLocalizedEnumValue(examApplication.Student?.StudyForm)] },
            {"{StudyProgram}", [examApplication.Student?.StudyProgram?.Name] },
            {"{StudyField}", [examApplication.Student?.StudyProgram?.StudyFieldName] },
            {"{Department}", [examApplication.Student?.Department?.Code] },
            {"{Supervisor}", [examApplication.Student?.Thesis?.Supervisor.User.DisplayName] },
            {"{StudyStartDate}", [examApplication.Student?.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy")] },
            {"{WrittenThesisTitle}", [examApplication.WrittenThesisTitle] },
            {"{WrittenThesisTitleEnglish}", [examApplication.WrittenThesisTitleEnglish] },
            {"{Subjects}", examApplication.Subjects.Select(s => s.Name).ToList<string?>() },
            {"{CurrentDate}", [examApplication.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}