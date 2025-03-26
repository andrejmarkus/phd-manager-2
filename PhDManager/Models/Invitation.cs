using System.ComponentModel.DataAnnotations;

namespace PhDManager.Models
{
    public class Invitation : BaseModel
    {
        public string Email { get; set; } = string.Empty;
        public string Guid { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
