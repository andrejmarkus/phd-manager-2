using PhDManager.Models.Roles;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Address : BaseModel
    {
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Birthplace { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; } = default!;

        [NotMapped]
        public string FullAddress => $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";
    }
}
