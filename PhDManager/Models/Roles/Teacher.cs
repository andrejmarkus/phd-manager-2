using PhDManager.Data;

namespace PhDManager.Models.Roles
{
    public class Teacher : BaseModel
    {
        public const string Role = "Učiteľ";

        public virtual Department? Department { get; set; }
        public virtual List<Thesis> Theses { get; set; } = [];
        public virtual List<Thesis> SpecialistTheses { get; set; } = [];
        public virtual List<ExamSupervisor> Examiners { get; set; } = [];
        public virtual List<ExamSupervisor> Chairpersons { get; set; } = [];
        public virtual List<ExamSupervisor> ExternalMembers { get; set; } = [];
        public virtual List<ExamSupervisor> AcademicCommitteeMembers { get; set; } = [];
        public virtual List<ExamSupervisor> AdditionalMembers { get; set; } = [];
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
