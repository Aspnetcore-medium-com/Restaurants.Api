using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;


namespace Restaurants.Core.Dishes.Commands.Create
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository, IDishesRepositroy dishesRepositroy, IMapper mapper) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("create dish request {@Request}", request);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogError("Restaurant id {Id} not found", request.RestaurantId);
                throw new RestaurantNotFoundException($"Restaurant id {request.RestaurantId} not found");
            }
            var dish = mapper.Map<Dish>(request);
            await dishesRepositroy.CreateDishAsync(dish);
            return dish.Id;
        }
    }
}
