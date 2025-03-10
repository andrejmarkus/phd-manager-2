using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class ExamApplication : BaseModel
    {
        public string WrittenThesisTitle { get; set; } = string.Empty;
        public string WrittenThesisTitleEnglish { get; set; } = string.Empty;
        public DateTime CurrentDate { get; set; } = DateTime.UtcNow;
        public virtual Student? Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = default!;
    }
}
