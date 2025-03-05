using PhDManager.Models.Enums;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Subject : BaseModel
    {
        public bool IsRequired { get; set; } = true;
        public char? Variant { get; set; }
        public string Name { get; set; } = string.Empty;
        public Semester Semester { get; set; }

        public int StudyProgramId { get; set; }
        [JsonIgnore]
        public virtual StudyProgram StudyProgram { get; set; } = default!;
        public virtual List<IndividualPlan> IndividualPlans { get; set; } = [];
        public virtual List<IndividualPlanSubject> IndividualPlanSubjects { get; set; } = [];
    }
}
