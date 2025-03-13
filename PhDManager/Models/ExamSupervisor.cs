using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class ExamSupervisor : BaseModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public string OpponentDisplayName { get; set; } = string.Empty;
        public string OpponentMail { get; set; } = string.Empty;
        public string OpponentPhoneNumber { get; set; } = string.Empty;
        public string OpponentDepartment { get; set; } = string.Empty;

        public virtual Student Student { get; set; } = default!;

        public virtual Teacher Examiner { get; set; } = default!;
        public virtual Teacher Chairperson { get; set; } = default!;
        public virtual Teacher ExternalMember { get; set; } = default!;
        public virtual Teacher AcademicCommitteeMember { get; set; } = default!;
        public virtual Teacher AdditionalMember { get; set; } = default!;
    }
}
