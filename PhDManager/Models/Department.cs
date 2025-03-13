using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class Department : BaseModel
    {
        public virtual List<Student> Students { get; set; } = [];
        public virtual List<Teacher> Teachers { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
