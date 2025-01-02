using PhDManager.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhDManager.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        [NotMapped]
        public string FullAddress => $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";
    }
}
