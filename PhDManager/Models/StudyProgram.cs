namespace PhDManager.Models
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
