using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetRestaurantsById
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantResponseDto?>
    {
        public int Id { get; set; } = id;
    }
}
