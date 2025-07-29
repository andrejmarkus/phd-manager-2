using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class DissertationDefenseSupervisorDocument(IJSRuntime jsRuntime, EnumService enumService, DissertationDefenseSupervisor dissertationDefenseSupervisor) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => dissertationDefenseSupervisor.Student.User.DisplayName + "_dp_skolitel";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "dissertation_defense_supervisor_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            {"{Student}", [dissertationDefenseSupervisor.Student.User.UserName] },
            {"{StudyForm}", [_enumService.GetLocalizedEnumValue(dissertationDefenseSupervisor.Student.StudyForm)] },
            {"{StudyProgram}", [dissertationDefenseSupervisor.Student.StudyProgram?.Name] },
            {"{StudyField}", [dissertationDefenseSupervisor.Student.StudyProgram?.StudyFieldName] },
            {"{Department}", [dissertationDefenseSupervisor.Student.Department?.Code] },
            {"{Supervisor}", [dissertationDefenseSupervisor.Student.Thesis?.Supervisor.User.DisplayName] },
            {"{StudyStartDate}", [dissertationDefenseSupervisor.Student.IndividualPlan?.StudyStartDate?.ToString("dd.MM.yyyy")] },
            {"{StudyEndDate}", [dissertationDefenseSupervisor.Student.IndividualPlan?.StudyEndDate?.ToString("dd.MM.yyyy")] },
            {"{CreditsCount}", [dissertationDefenseSupervisor.CreditsCount.ToString()] },
            {"{ApplicationYear}", [dissertationDefenseSupervisor.ApplicationYear.ToString()] },
            {"{ThesisTitle}", [dissertationDefenseSupervisor.Student.Thesis?.Title] },
            {"{Opponent1}", [dissertationDefenseSupervisor.OpponentDisplayNames[0]] },
            {"{Opponent1WorkplaceAddress}", [dissertationDefenseSupervisor.OpponentWorkplaceAddresses[0]] },
            {"{Opponent1PhoneNumber}", [dissertationDefenseSupervisor.OpponentPhoneNumbers[0]] },
            {"{Opponent1Mail}", [dissertationDefenseSupervisor.OpponentEmails[0]] },
            {"{Opponent2}", [dissertationDefenseSupervisor.OpponentDisplayNames[1]] },
            {"{Opponent2WorkplaceAddress}", [dissertationDefenseSupervisor.OpponentWorkplaceAddresses[1]] },
            {"{Opponent2PhoneNumber}", [dissertationDefenseSupervisor.OpponentPhoneNumbers[1]] },
            {"{Opponent2Mail}", [dissertationDefenseSupervisor.OpponentEmails[1]] },
            {"{Opponent3}", [dissertationDefenseSupervisor.OpponentDisplayNames[2]] },
            {"{Opponent3WorkplaceAddress}", [dissertationDefenseSupervisor.OpponentWorkplaceAddresses[2]] },
            {"{Opponent3PhoneNumber}", [dissertationDefenseSupervisor.OpponentPhoneNumbers[2]] },
            {"{Opponent3Mail}", [dissertationDefenseSupervisor.OpponentEmails[2]] },
            {"{CurrentDate}", [dissertationDefenseSupervisor.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}