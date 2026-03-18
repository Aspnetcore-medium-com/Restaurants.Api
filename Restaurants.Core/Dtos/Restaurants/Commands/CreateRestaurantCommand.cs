using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Commands
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; } = string.Empty;
        public string? ContactNumber { get; set; } = string.Empty;
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
