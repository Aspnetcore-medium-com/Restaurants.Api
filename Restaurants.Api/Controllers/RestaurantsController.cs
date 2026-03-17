using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Core.Dtos.Restaurants;
using Restaurants.Core.ServiceContracts;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;
        public RestaurantsController(IRestaurantsService restaurantsService) { 
            _restaurantsService = restaurantsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
           IReadOnlyList<RestaurantResponseDto> restaurants = await _restaurantsService.GetAllRestaurants(cancellationToken);
           return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            RestaurantResponseDto restaurantResponseDto = await _restaurantsService.GetRestaurantById(id, cancellationToken);
            return Ok(restaurantResponseDto);
        }
    }
}
