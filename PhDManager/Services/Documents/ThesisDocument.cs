using Microsoft.JSInterop;
using PhDManager.Models;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class ThesisDocument(IJSRuntime jsRuntime, EnumService enumService, Thesis thesis) : DocumentTemplate(jsRuntime, enumService)
{
    protected override string DocumentName => thesis.Title;

    protected override string TemplatePath =>
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "thesis_template.docx");

    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            { "{Title}", [thesis.Title] },
            { "{Supervisor}", [thesis.Supervisor.User.DisplayName] },
            { "{SupervisorSpecialist}", [thesis.SupervisorSpecialist?.User.DisplayName] },
            { "{StudyProgram}", [thesis.StudyProgram.Name] },
            { "{StudyField}", [thesis.StudyProgram.StudyFieldName] },
            { "{DailyStudy}", [thesis.DailyStudy ? "☑" : "☐"] },
            { "{ExternalStudy}", [thesis.ExternalStudy ? "☑" : "☐"] },
            { "{Subjects}", thesis.StudyProgram.ThesisSubjects.ToList<string?>() },
            { "{Description}", [thesis.Description] },
            { "{ScientificContribution}", [thesis.ScientificContribution] },
            { "{ScientificProgress}", [thesis.ScientificProgress] },
            { "{ResearchType}", [thesis.ResearchType] },
            { "{ResearchTask}", [thesis.ResearchTask] },
            { "{SolutionResults}", [thesis.SolutionResults] }
        };
    }
}