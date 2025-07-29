using System.Text.Json.Serialization;
using PhDManager.Models.Documents;

namespace PhDManager.Models
{
    public class StudyProgram : BaseModel
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string StudyFieldName { get; set; } = string.Empty;
        public string[] ThesisSubjects { get; set; } = [];

        public virtual List<Subject> Subjects { get; set; } = [];
        [JsonIgnore]
        public virtual List<Thesis> Theses { get; set; } = [];
    }
}
