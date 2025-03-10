using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models.Roles
{
    public class ExternalTeacher : BaseModel
    {
        [NotMapped]
        public const string Role = "Externý učiteľ";

        public virtual List<Comment> Comments { get; set; } = [];
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
