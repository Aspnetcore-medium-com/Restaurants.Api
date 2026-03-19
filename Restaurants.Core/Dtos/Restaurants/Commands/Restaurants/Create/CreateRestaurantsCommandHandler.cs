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

namespace Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create
{
    public class CreateRestaurantsCommandHandler : IRequestHandler<CreateRestaurantsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<CreateRestaurantsCommandHandler> _logger;
        public CreateRestaurantsCommandHandler(IMapper mapper, IRestaurantRepository restaurantRepository, ILogger<CreateRestaurantsCommandHandler> logger)  {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }
        public async Task<int> Handle(CreateRestaurantsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("create restaurant called");
            var restaurant = _mapper.Map<Restaurant>(request);
            var id = await _restaurantRepository.CreateRestaurantAsync(restaurant, cancellationToken);
            _logger.LogInformation($"restaurant created with id {id}");
            return id;
        }
    }
}
