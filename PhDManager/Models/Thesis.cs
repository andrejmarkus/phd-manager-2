using PhDManager.Data;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Thesis
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; } = false;
        public string Title { get; set; } = string.Empty;
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string? TitleEnglish { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? DescriptionEnglish { get; set; }
        public string ScientificContribution { get; set; } = string.Empty;
        public string ScientificProgress { get; set; } = string.Empty;
        public string ResearchType { get; set; } = string.Empty;
        public string ResearchTask { get; set; } = string.Empty;
        public string SolutionResults { get; set; } = string.Empty;
        public string StudyFieldName { get; set; } = string.Empty;
        public bool DailyStudy { get; set; } = false;
        public bool ExternalStudy { get; set; } = false;
        public List<string> SubjectNames { get; set; } = new();
        public string SupervisorId { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ApplicationUser Supervisor { get; set; }
        public int StudyProgramId { get; set; }
        public virtual StudyProgram StudyProgram { get; set; }

        public virtual List<Comment> Comments { get; set; } = new();

        [JsonIgnore]
        public virtual IndividualPlan? IndividualPlan { get; set; }
    }
}
