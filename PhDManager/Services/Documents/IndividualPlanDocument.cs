using Microsoft.JSInterop;
using PhDManager.Models.Documents;
using PhDManager.Models.Enums;

namespace PhDManager.Services.Documents;

public class IndividualPlanDocument(IJSRuntime jsRuntime, EnumService enumService, IndividualPlan individualPlan) : DocumentTemplate(jsRuntime, enumService)
{
    private readonly EnumService _enumService = enumService;

    protected override string DocumentName => individualPlan.Student?.User.DisplayName + "_individualny_plan";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "individual_plan_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            {"{Student}", [individualPlan.Student?.User.DisplayName] },
            {"{Birthdate}", [individualPlan.Student?.User.Birthdate?.ToString("dd.MM.yyyy")] },
            {"{FullAddress}", [individualPlan.Student?.Address?.FullAddress] },
            {"{PhoneNumber}", [individualPlan.Student?.User.PhoneNumber] },
            {"{StudyForm}", [_enumService.GetLocalizedEnumValue(individualPlan.Student?.StudyForm)] },
            {"{StudyProgram}", [individualPlan.Student?.Thesis?.StudyProgram.Name] },
            {"{StudyField}", [individualPlan.Student?.Thesis?.StudyProgram.StudyFieldName] },
            {"{Supervisor}", [individualPlan.Student?.Thesis?.Supervisor.User.DisplayName] },
            {"{StudyStartDate}", [individualPlan.StudyStartDate?.ToString("dd.MM.yyyy")] },
            {"{DissertationExamDate}", [individualPlan.DissertationApplicationDate?.ToString("dd.MM.yyyy")] },
            {"{DissertationSubmissionDate}", [individualPlan.DissertationSubmissionDate?.ToString("MMMM yyyy")] },
            {"{StudyEndDate}", [individualPlan.StudyEndDate?.ToString("dd.MM.yyyy")] },
            {"{Subjects}", individualPlan.Subjects.Where(s => s.RequirementLevel == RequirementLevel.Compulsory).Select(s => s.Name).ToList<string?>() },
            {"{OptionalSubjects}", individualPlan.Subjects.Where(s => s.RequirementLevel != RequirementLevel.Compulsory).Select(s => s.Name).ToList<string?>() },
            {"{WrittenThesisTitle}", [individualPlan.WrittenThesisTitle] },
            {"{WrittenThesisRequiredLiterature}", [individualPlan.WrittenThesisRequiredLiterature] },
            {"{WrittenThesisRecommendedLiterature}", [individualPlan.WrittenThesisRecommendedLiterature] },
            {"{WrittenThesisRecommendedLectures}", [individualPlan.WrittenThesisRecommendedLectures] },
            {"{ThesisTitle}", [individualPlan.Student?.Thesis?.Title] },
            {"{ThesisReasearchTask}", [individualPlan.Student?.Thesis?.ResearchTask] },
            {"{ThesisSolutionResults}", [individualPlan.Student?.Thesis?.SolutionResults] },
            {"{Tasks}", individualPlan.Tasks.ToList<string?>() },
            {"{TaskDeadlines}", individualPlan.TaskDeadlines.Select(d => (d ?? DateTime.Now).ToString("MMMM yyyy")).ToList<string?>() },
            {"{CurrentDate}", [individualPlan.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}