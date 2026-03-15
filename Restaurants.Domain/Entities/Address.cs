
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Address
    {
        [StringLength(100)]
        public string City { get; set; } = default!;
        [StringLength(100)]
        public string? Street { get; set; }
        [StringLength(50)]
        public string? PostalCode { get; set; }
    }
}
