using AutoMapper;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;
using Restaurants.Infrastructure.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Delete
{
    public class DeleteRestaurantRequestHandler(IMapper mapper, IRestaurantsRepository restaurantRepository, 
        ILogger<DeleteRestaurantRequestHandler> logger,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantRequest, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{Class}{Method} called", nameof(DeleteRestaurantRequestHandler), nameof(Handle));
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);
            if (restaurant == null)
            {
                return false;
            }
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                logger.LogInformation("Delete operation denied {Restaurant}",restaurant.Name );
                throw new ForbiddenException();
            }
            await restaurantRepository.DeleteRestaurant(restaurant.Id);
            return true;
        }
    }
}
