using PhDManager.Data;

namespace PhDManager.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // public bool IsResolved { get; set; } = false;
        public string Text { get; set; } = default!;
        public DateTime Created { get; set; }

        public string ReviewerId { get; set; } = default!;
        public virtual ApplicationUser Reviewer { get; set; } = default!;

        public int ThesisId { get; set; }
        public virtual Thesis Thesis { get; set; } = default!;
    }
}
