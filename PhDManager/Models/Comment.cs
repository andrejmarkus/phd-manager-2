using PhDManager.Data;

namespace PhDManager.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // public bool IsResolved { get; set; } = false;
        public string Text { get; set; }
        public DateTime Created { get; set; }

        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }

        public int ThesisId { get; set; }
        public Thesis Thesis { get; set; }
    }
}
