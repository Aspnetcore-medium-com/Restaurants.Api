using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Update
{
    public class UpdateRestaurantCommandHandler(IMapper mapper, IRestaurantsRepository restaurantRepository, ILogger<UpdateRestaurantCommandHandler> logger) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("update called {@request}", request);
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);
            if (restaurant == null) return false;
            restaurant.Name = request.Name;
            restaurant.Description = request.Description;
            restaurant.HasDelivery = request.HasDelivery;
            await restaurantRepository.UpdateRestaurantAsync(restaurant);
            return true;
        }
    }
}
