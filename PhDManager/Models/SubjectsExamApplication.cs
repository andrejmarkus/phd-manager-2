namespace PhDManager.Models
{
    public class SubjectsExamApplication : BaseModel
    {
        public virtual Student? Student { get; set; } = default!;
        public virtual List<Subject> Subjects { get; set; } = default!;
    }
}
