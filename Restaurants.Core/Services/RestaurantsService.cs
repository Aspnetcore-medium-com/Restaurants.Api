using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Dtos.Restaurants;
using Restaurants.Core.ServiceContracts;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Services
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly ILogger<RestaurantsService> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantsService(IRestaurantRepository restaurantRepository, ILogger<RestaurantsService> logger, IMapper mapper) {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<RestaurantResponseDto>> GetAllRestaurants(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Service}.{Method} called", nameof(RestaurantsService), nameof(GetAllRestaurants));
            IReadOnlyList<Restaurant> restaurants = await _restaurantRepository.GetRestaurantsAsync(cancellationToken);
            _logger.LogInformation("Fetched {count} from restaurants", restaurants.Count);
            return _mapper.Map<IReadOnlyList<RestaurantResponseDto>>(restaurants);
        }

        public async Task<RestaurantResponseDto> GetRestaurantById(int restaurantId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Service}.{Method} called for {RestId}", nameof(RestaurantsService), nameof(GetRestaurantById), restaurantId);
            Restaurant? restaurant = await _restaurantRepository.GetRestaurantByIdAsync(restaurantId, cancellationToken);
            if (restaurant == null) { 
                throw new KeyNotFoundException($"Restaurant id {restaurantId} not found");
            }
            return _mapper.Map<RestaurantResponseDto>(restaurant);
        }

        public async Task<int> CreateRestaurant(RestaurantRequestDto restaurantRequestDto,CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("create restaurant called");
            var restaurant = _mapper.Map<Restaurant>(restaurantRequestDto);
            var id = await _restaurantRepository.CreateRestaurantAsync(restaurant, cancellationToken);
            _logger.LogInformation($"restaurant created with id {id}");
            return id;
        }
    }
}
