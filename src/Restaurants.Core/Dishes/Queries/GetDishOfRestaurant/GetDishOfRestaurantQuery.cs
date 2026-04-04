using MediatR;
using Restaurants.Core.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.Queries.GetDishOfRestaurant
{
    public class GetDishOfRestaurantQuery(int restaurantId, int dishId) : IRequest<DishResponseDto>
    {
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; }  = dishId;
    }
}
