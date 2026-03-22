using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;


namespace Restaurants.Core.Dishes.Commands.Delete
{
    public class DeleteDishesCommandHandler(ILogger<DeleteDishesCommandHandler> logger,
        IDishesRepositroy dishesRepository, IRestaurantsRepository restaurantsRepository, IMapper mapper
        ) : IRequestHandler<DeleteDishesCommand>
    {
        public async Task Handle(DeleteDishesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("delete dish request {@Request}", request);
            var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                logger.LogError("Restaurant id {Id} not found", request.RestaurantId);
                throw new NotFoundException($"Restaurant id {request.RestaurantId} not found");
            }
            if (!await dishesRepository.HasDishesAsync(request.RestaurantId))
            {
                logger.LogError("Restaurant id {Id} dishes not found", request.RestaurantId);
                throw new NotFoundException($"Restaurant id {request.RestaurantId} dishes not found");
            }
            await dishesRepository.DeleteDishesAsync(restaurant.Dishes!);
        }
    }
}
