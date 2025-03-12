using PhDManager.Data;
using PhDManager.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models.Roles
{
    public class Student : BaseModel
    {
        public const string Role = "Študent";

        public string StartSchoolYear { get; set; } = string.Empty;
        public string EndSchoolYear { get; set; } = string.Empty;
        public StudyForm StudyForm { get; set; } = StudyForm.Daily;
        public StudentState State { get; set; } = StudentState.Study;
        public string DissertationExamResult { get; set; } = string.Empty;
        public string DissertationExamTranscript { get; set; } = string.Empty;
        public DateTime? DissertationExamDate { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Address? Address { get; set; }
        public virtual StudyProgram? StudyProgram { get; set; }
        public virtual Thesis? ThesisInterest { get; set; }
        public virtual Thesis? Thesis { get; set; }
        public virtual IndividualPlan? IndividualPlan { get; set; }
        public virtual SubjectsExamApplication? SubjectsExamApplication { get; set; }
        public virtual ExamApplication? ExamApplication { get; set; }
        public virtual StudentEvaluation? StudentEvaluation { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;

        [NotMapped]
        public bool HasInfo => User.Birthdate is not null && Address is not null && StudyProgram is not null;
    }
}
