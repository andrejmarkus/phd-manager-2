namespace PhDManager.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Guid { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
