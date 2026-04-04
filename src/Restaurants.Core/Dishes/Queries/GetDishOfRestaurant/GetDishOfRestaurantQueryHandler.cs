using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Dishes.Dtos;
using Restaurants.Core.Dishes.Queries.GetDishes;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.Queries.GetDishOfRestaurant
{
    public class GetDishOfRestaurantQueryHandler(ILogger<GetDishOfRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishOfRestaurantQuery, DishResponseDto>
    {
        public async Task<DishResponseDto> Handle(GetDishOfRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{Query}.{Handler} called", nameof(GetDishOfRestaurantQueryHandler), nameof(Handle));

            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId, cancellationToken);
            if (restaurant == null)
            {
                logger.LogError("restaurant not found {RestaurantId}", request.RestaurantId);
                throw new NotFoundException("restaurant not found ",request.RestaurantId);
            }
            var dish = restaurant.Dishes?.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null)
            {
                logger.LogError("dish {Dishid} not found in restaurant {RestaurantId} ", request.DishId, request.RestaurantId);
                throw new NotFoundException("Dish not found ", request.DishId);

            }
            return mapper.Map<DishResponseDto>(dish);

        }
    }
}
