using PhDManager.Data;

namespace PhDManager.Models.DTOs
{
    public class UserInfo(ApplicationUser user, string role)
    {
        public ApplicationUser User { get; set; } = user;
        public string Role { get; set; } = role;
    }
}
