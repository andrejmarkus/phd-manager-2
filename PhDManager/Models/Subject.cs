namespace PhDManager.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int Credits { get; set; }

        public virtual StudyProgram StudyProgram { get; set; } = default!;
        public virtual List<Thesis> Theses { get; set; } = new List<Thesis>();
    }
}
