using PhDManager.Data;

namespace PhDManager.Models
{
    public class Thesis
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleEnglish { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? DescriptionEnglish { get; set; }
        public string ScientificContribution { get; set; } = string.Empty;
        public string ScientificProgress { get; set; } = string.Empty;
        public string ResearchType { get; set; } = string.Empty;
        public string ResearchTask { get; set; } = string.Empty;
        public string SolutionResults { get; set; } = string.Empty;

        public bool DailyStudy { get; set; } = false;
        public bool ExternalStudy { get; set; } = false;

        public string SupervisorId { get; set; }
        public ApplicationUser? Supervisor { get; set; }

        public StudyProgram StudyProgram { get; set; }

        public Subject[] Subjects { get; set; } = { };
    }
}
