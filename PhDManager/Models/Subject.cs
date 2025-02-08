using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int Credits { get; set; }

        public int StudyProgramId { get; set; }

        [JsonIgnore]
        public virtual StudyProgram StudyProgram { get; set; }
        public virtual List<IndividualPlan> IndividualPlans { get; set; }
    }
}
