using PhDManager.Models.Roles;

namespace PhDManager.Models
{
    public class DissertationDefenseSupervisor : BaseModel
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public int CreditsCount { get; set; }
        public int ApplicationYear { get; set; }
        public string[] OpponentDisplayNames { get; set; } = [];
        public string[] OpponentWorkplaceAddresses { get; set; } = [];
        public string[] OpponentPhoneNumbers { get; set; } = [];
        public string[] OpponentEmails { get; set; } = [];

        public virtual Student Student { get; set; } = default!;
    }
}
