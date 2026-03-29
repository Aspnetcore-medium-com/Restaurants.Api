using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Users.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Exceptions;
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
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly ILogger<CreateRestaurantsCommandHandler> _logger;
        private readonly IUserContext _userContext;
        public CreateRestaurantsCommandHandler(IMapper mapper, IRestaurantsRepository restaurantRepository, 
            ILogger<CreateRestaurantsCommandHandler> logger, IUserContext userContext)  {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _userContext = userContext;
        }
        public async Task<int> Handle(CreateRestaurantsCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("create restaurant called {@Request}",request);
            var restaurant = _mapper.Map<Restaurant>(request);
            if ( user == null) { throw new NotFoundException($"user not found ",nameof(user)); }
            restaurant.OwnerId = Guid.Parse(user.Id);
            var id = await _restaurantRepository.CreateRestaurantAsync(restaurant, cancellationToken);
            _logger.LogInformation("restaurant created with id {Id}",id);
            return id;
        }
    }
}
