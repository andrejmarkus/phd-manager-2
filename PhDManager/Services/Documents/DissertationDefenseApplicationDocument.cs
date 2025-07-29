using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class DissertationDefenseApplicationDocument(IJSRuntime jsRuntime, EnumService enumService, DissertationDefenseApplication dissertationDefenseApplication) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => dissertationDefenseApplication.Student.User.DisplayName + "_ds_ziadost";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "dissertation_defense_application_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            {"{Student}", [dissertationDefenseApplication.Student.User.UserName] },
            {"{Birthdate}", [dissertationDefenseApplication.Student.User.Birthdate?.ToString("dd.MM.yyyy")] },
            {"{Birthplace}", [dissertationDefenseApplication.Student.Address?.Birthplace] },
            {"{FullAddress}", [dissertationDefenseApplication.Student.Address?.FullAddress] },
            {"{Nationality}", [dissertationDefenseApplication.Nationality] },
            {"{Ethnicity}", [dissertationDefenseApplication.Ethnicity] },
            {"{AchievedHigherEducation}", [dissertationDefenseApplication.AchievedHigherEducation] },
            {"{StudyForm}", [_enumService.GetLocalizedEnumValue(dissertationDefenseApplication.Student.StudyForm)] },
            {"{StudyProgram}", [dissertationDefenseApplication.Student.StudyProgram?.Name] },
            {"{StudyField}", [dissertationDefenseApplication.Student.StudyProgram?.StudyFieldName] },
            {"{Department}", [dissertationDefenseApplication.Student.Department?.Code] },
            {"{Supervisor}", [dissertationDefenseApplication.Student.Thesis?.Supervisor.User.DisplayName] },
            {"{StudyStartDate}", [dissertationDefenseApplication.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy")] },
            {"{ApplicationSubmissionYear}", [dissertationDefenseApplication.ApplicationSubmissionYear.ToString()] },
            {"{ThesisTitle}", [dissertationDefenseApplication.Student.Thesis?.Title] },
            {"{CurrentDate}", [dissertationDefenseApplication.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}