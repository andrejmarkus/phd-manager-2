namespace PhDManager.Models
{
    public class ExamApplication : BaseModel
    {
        public string WrittenThesisTitle { get; set; } = string.Empty;
        public string WrittenThesisTitleEnglish { get; set; } = string.Empty;
        public virtual Student? Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = default!;
    }
}
