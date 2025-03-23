using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class SubjectsExamApplication : BaseModel
    {
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public virtual Student? Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = [];
    }
}
