using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class StudyProgram
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string StudyFieldName { get; set; } = string.Empty;

        public virtual List<Subject> Subjects { get; set; } = [];
        [JsonIgnore]
        public virtual List<Thesis> Thesis { get; set; } = [];
    }
}
