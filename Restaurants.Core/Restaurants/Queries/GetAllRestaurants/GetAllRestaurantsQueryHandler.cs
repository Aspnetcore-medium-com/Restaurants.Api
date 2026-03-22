using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IReadOnlyList<RestaurantResponseDto>>
    {
        private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly IMapper _mapper;
        
        public GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IRestaurantsRepository restaurantRepository, IMapper mapper) {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<RestaurantResponseDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Service}.{Method} called", nameof(GetAllRestaurantsQueryHandler), "GetAllRestaurants");
            IReadOnlyList<Restaurant> restaurants = await _restaurantRepository.GetRestaurantsAsync(cancellationToken);
            _logger.LogInformation("Fetched {count} from restaurants", restaurants.Count);
            return _mapper.Map<IReadOnlyList<RestaurantResponseDto>>(restaurants);
        }
    }
}
