using PhDManager.Models.Enums;

namespace PhDManager.Models
{
    public class IndividualPlanSubjectGrade
    {
        public int IndividualPlanId { get; set; }
        public virtual IndividualPlan IndividualPlan { get; set; } = default!;
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = default!;
        public Grade Grade { get; set; }
    }
}
