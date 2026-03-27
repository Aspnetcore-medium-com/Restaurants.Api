using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Dishes.Dtos;
using Restaurants.Core.Dishes.Queries.GetDishesOfRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.Queries.GetDishes
{
    public class GetDishesOfRestaurantQueryHandler(ILogger<GetDishesOfRestaurantQueryHandler> logger,
        IMapper mapper, IRestaurantsRepository restaurantsRepository
            ) : IRequestHandler<GetDishesOfRestaurantQuery, IEnumerable<DishResponseDto>>
    {
        public async Task<IEnumerable<DishResponseDto>> Handle(GetDishesOfRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{Query}.{Handler} called", nameof(GetDishesOfRestaurantQueryHandler), nameof(Handle));
            var restaurantId = request.RestaurantId;
            Restaurant? restaurant = await restaurantsRepository.GetRestaurantByIdAsync(restaurantId,cancellationToken);
            if (restaurant == null) {
                logger.LogError("restaurant not found {RestaurantId}",restaurantId);
                throw new NotFoundException($"restaurant not found",restaurantId); 
            }
            return mapper.Map<List<DishResponseDto>>(restaurant.Dishes);
        }
    }
}
