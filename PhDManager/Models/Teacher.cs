using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhDManager.Models
{
    public class Teacher : BaseModel
    {
        [NotMapped]
        public const string Role = "Učiteľ";

        public virtual List<Thesis> Theses { get; set; } = [];
        public virtual List<Thesis> SpecialistTheses { get; set; } = [];
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
