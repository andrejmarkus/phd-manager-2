using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Thesis : BaseModel
    {
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
        public string SchoolYear { get; set; } = string.Empty;
        public bool DailyStudy { get; set; } = false;
        public bool ExternalStudy { get; set; } = false;
        public string SupervisorId { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual List<Student> InterestedStudents { get; set; } = [];
        [JsonIgnore]
        public virtual Student? Student { get; set; }
        [JsonIgnore]
        public virtual Teacher Supervisor { get; set; } = default!;
        [JsonIgnore]
        public virtual Teacher? SupervisorSpecialist { get; set; }
        public int StudyProgramId { get; set; }
        public virtual StudyProgram StudyProgram { get; set; } = default!;

        public virtual List<Comment> Comments { get; set; } = [];
    }
}
