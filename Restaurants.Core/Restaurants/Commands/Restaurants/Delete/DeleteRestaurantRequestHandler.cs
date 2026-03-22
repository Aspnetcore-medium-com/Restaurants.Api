using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Delete
{
    public class DeleteRestaurantRequestHandler(IMapper mapper, IRestaurantsRepository restaurantRepository, ILogger<DeleteRestaurantRequestHandler> logger) : IRequestHandler<DeleteRestaurantRequest, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("{class}{method} called", nameof(DeleteRestaurantRequestHandler), nameof(Handle));
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);
            if (restaurant == null)
            {
                return false;
            }
            await restaurantRepository.DeleteRestaurant(restaurant.Id);
            return true;
        }
    }
}
