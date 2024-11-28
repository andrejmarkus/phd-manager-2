using PhDManager.Data;
using System.ComponentModel.DataAnnotations;

namespace PhDManager.Models
{
    public class Thesis
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; } = false;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string? TitleEnglish { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public string? DescriptionEnglish { get; set; }
        [Required]
        public string ScientificContribution { get; set; } = string.Empty;
        [Required]
        public string ScientificProgress { get; set; } = string.Empty;
        [Required]
        public string ResearchType { get; set; } = string.Empty;
        [Required]
        public string ResearchTask { get; set; } = string.Empty;
        [Required]
        public string SolutionResults { get; set; } = string.Empty;
        public bool DailyStudy { get; set; } = false;
        public bool ExternalStudy { get; set; } = false;
        [Required]
        public string SupervisorId { get; set; } = string.Empty;
        public ApplicationUser Supervisor { get; set; }
        [Required]
        public int StudyProgramId { get; set; }
        public StudyProgram StudyProgram { get; set; }

        public Subject[] Subjects { get; set; } = { };

        public List<Comment> Comments { get; set; }
    }
}
