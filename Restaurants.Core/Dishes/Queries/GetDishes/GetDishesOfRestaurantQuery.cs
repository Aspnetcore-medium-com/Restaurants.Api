

using MediatR;
using Restaurants.Core.Dishes.Dtos;

namespace Restaurants.Core.Dishes.Queries.GetDishesOfRestaurant
{
    public class GetDishesOfRestaurantQuery(int restaurantId): IRequest<IEnumerable<DishResponseDto>>
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
