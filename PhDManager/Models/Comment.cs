using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class Comment : BaseModel
    {
        // public bool IsResolved { get; set; } = false;
        public string Text { get; set; } = default!;
        public DateTime Created { get; set; }

        public int ExternalId { get; set; }
        public virtual ExternalTeacher External { get; set; } = default!;

        public int ThesisId { get; set; }
        public virtual Thesis Thesis { get; set; } = default!;
    }
}
