using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Dtos.Restaurants;
using Restaurants.Core.ServiceContracts;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Services
{
    public class RestaurantService : IRestaurantsService
    {
        private readonly ILogger<RestaurantService> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger, IMapper mapper) {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<RestaurantResponseDto>> GetAllRestaurants(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{service}.{method} called", nameof(RestaurantService), nameof(GetAllRestaurants));
            IReadOnlyList<Restaurant> restaurants = await _restaurantRepository.GetRestaurantsAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<RestaurantResponseDto>>(restaurants);
            _logger.LogInformation("Fetched {count} from restaurants",restaurants.Count);
        }
    }
}
