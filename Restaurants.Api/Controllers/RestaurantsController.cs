using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Filters;
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
        public async Task<ActionResult<IReadOnlyList<RestaurantResponseDto>>> GetAll(CancellationToken cancellationToken = default)
        {
           IReadOnlyList<RestaurantResponseDto> restaurants = await _restaurantsService.GetAllRestaurants(cancellationToken);
           return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantResponseDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            RestaurantResponseDto restaurantResponseDto = await _restaurantsService.GetRestaurantById(id, cancellationToken);
            return Ok(restaurantResponseDto);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidationFilter<RestaurantRequestDto>))]
        public async Task<ActionResult<RestaurantRequestDto>> Create(RestaurantRequestDto restaurantRequestDto, CancellationToken cancellationToken = default)
        {
            var id =  await _restaurantsService.CreateRestaurant(restaurantRequestDto, cancellationToken);
            var createdRestaurant = await _restaurantsService.GetRestaurantById(id,cancellationToken); 
            return CreatedAtAction(nameof(GetById), new { id }, createdRestaurant);
        }
    }
}
