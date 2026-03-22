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

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetRestaurantsById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
        
        public GetRestaurantByIdQueryHandler(IMapper mapper,IRestaurantsRepository restaurantRepository,ILogger<GetRestaurantByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }
        public async Task<RestaurantResponseDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Service}.{Method} called for {RestId}", nameof(RestaurantsService), "GetRestaurantById", request.Id);
            Restaurant? restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id, cancellationToken);
            if (restaurant == null)
            {
                return null;
            }
            return _mapper.Map<RestaurantResponseDto>(restaurant);
        }
    }
}
