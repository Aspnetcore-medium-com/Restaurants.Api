using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Filters;
using Restaurants.Core.Dishes.Commands.Create;
using Restaurants.Core.Dishes.Dtos;
using Restaurants.Core.Dishes.Queries.GetDishesOfRestaurant;
using Restaurants.Core.Dishes.Queries.GetDishOfRestaurant;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurant/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [TypeFilter(typeof(ValidationFilter<CreateDishCommand>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> Create(int restaurantId, CreateDishCommand  createDishCommand, CancellationToken cancellationToken = default)
        {
            createDishCommand.RestaurantId = restaurantId;
            var dishId = await mediator.Send(createDishCommand,cancellationToken);
            var dish = await mediator.Send(new GetDishOfRestaurantQuery(restaurantId, dishId), cancellationToken);
            return CreatedAtAction(nameof(GetDishOfRestaurant),new {restaurantId,dishId},dish);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DishResponseDto>>> GetDishes(int restaurantId, CancellationToken cancellationToken = default)
        {
           return Ok( await mediator.Send(new GetDishesOfRestaurantQuery(restaurantId),cancellationToken ));
        }


        [HttpGet("{dishId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DishResponseDto>> GetDishOfRestaurant(int restaurantId,int dishId, CancellationToken cancellationToken = default)
        {
            return Ok(await mediator.Send(new GetDishOfRestaurantQuery(restaurantId,dishId),cancellationToken));
        }
       
    }
}
