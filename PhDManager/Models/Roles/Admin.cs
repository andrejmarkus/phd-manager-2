using PhDManager.Data;

namespace PhDManager.Models.Roles
{
    public class Admin : BaseModel
    {
        public const string Role = "Admin";

        public virtual ApplicationUser User { get; set; } = default!;
    }
}
