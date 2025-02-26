using PhDManager.Data;

namespace PhDManager.Models
{
    public class Comment : BaseModel
    {
        // public bool IsResolved { get; set; } = false;
        public string Text { get; set; } = default!;
        public DateTime Created { get; set; }

        public string ExternalId { get; set; } = default!;
        public virtual External External { get; set; } = default!;

        public int ThesisId { get; set; }
        public virtual Thesis Thesis { get; set; } = default!;
    }
}
