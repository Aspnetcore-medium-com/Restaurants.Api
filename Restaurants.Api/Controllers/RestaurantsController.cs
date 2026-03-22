using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Filters;
using Restaurants.Core.Dtos.Restaurants;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Delete;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Update;
using Restaurants.Core.Dtos.Restaurants.Queries;
using Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Core.Dtos.Restaurants.Queries.GetRestaurantsById;
using Restaurants.Core.ServiceContracts;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;
        private readonly IMediator _mediator;
        public RestaurantsController(IRestaurantsService restaurantsService,IMediator mediator) { 
            _restaurantsService = restaurantsService;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<RestaurantResponseDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            IReadOnlyList<RestaurantResponseDto> restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantResponseDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            RestaurantResponseDto? restaurantResponseDto = await _mediator.Send(new GetRestaurantByIdQuery(id) );
            if (restaurantResponseDto == null)
            {
                return NotFound();
            } 
            return Ok(restaurantResponseDto);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidationFilter<CreateRestaurantsCommand>))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<RestaurantResponseDto>> Create(CreateRestaurantsCommand createRestaurantCommand, CancellationToken cancellationToken = default)
        {
            var id = await _mediator.Send(createRestaurantCommand, cancellationToken);
            var createdRestaurant = await _mediator.Send(new GetRestaurantByIdQuery(id), cancellationToken);
            if (createdRestaurant == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetById), new { id }, createdRestaurant);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteRestaurantRequest(id), cancellationToken);
            if (!response) return NotFound("Restaurant not found");
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id,UpdateRestaurantCommand updateRestaurantCommand,CancellationToken cancellation)
        {
            updateRestaurantCommand.Id = id;
            var response = await _mediator.Send(updateRestaurantCommand);
            if (!response) return NotFound($"Restaurant {updateRestaurantCommand.Id} not found");
            return NoContent();
        }
    }
}
