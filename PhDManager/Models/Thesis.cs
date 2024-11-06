namespace PhDManager.Models
{
    public class Thesis
    {
        public int ThesisId { get; set; }
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
