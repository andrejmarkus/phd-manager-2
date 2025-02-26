using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models
{
    public class External : BaseModel
    {
        [NotMapped]
        public const string Role = "Externista";

        public virtual List<Comment> Comments { get; set; } = [];
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
