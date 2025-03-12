using PhDManager.Data;

namespace PhDManager.Models.Roles
{
    public class Teacher : BaseModel
    {
        public const string Role = "Učiteľ";

        public virtual List<Thesis> Theses { get; set; } = [];
        public virtual List<Thesis> SpecialistTheses { get; set; } = [];

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
