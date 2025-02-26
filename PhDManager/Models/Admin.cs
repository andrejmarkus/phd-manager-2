using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models
{
    public class Admin : BaseModel
    {
        [NotMapped]
        public const string Role = "Admin";

        public virtual ApplicationUser User { get; set; } = default!;
    }
}
