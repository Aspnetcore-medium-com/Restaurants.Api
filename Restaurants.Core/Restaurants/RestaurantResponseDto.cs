using Restaurants.Core.Dtos.Dishes;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants
{
    public class RestaurantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishResponseDto>? Dishes { get; set; } = new();
    }
}
