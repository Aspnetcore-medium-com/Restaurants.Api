

using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = default!;
        [StringLength(100)]
        public string Description { get; set; } = default!;
        [StringLength(50)]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        [StringLength(50)]
        public string? ContactEmail { get; set; } = string.Empty;
        [StringLength(50)]
        public string? ContactNumber {  get; set; } = string.Empty;
        public Address? Address { get; set; }
        public List<Dish> Dishes { get; set; } = new();
    }
}
