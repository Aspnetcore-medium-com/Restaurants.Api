using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Api.Filters;
using Restaurants.Core.Dishes.Commands.Create;
using Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Create;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurant/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [TypeFilter(typeof(ValidationFilter<CreateDishCommand>))]
        public async Task<ActionResult<int>> Create(int restaurantId, CreateDishCommand  createDishCommand, CancellationToken cancellationToken = default)
        {
            createDishCommand.RestaurantId = restaurantId;
            var dishId = await mediator.Send(createDishCommand,cancellationToken);
            return Created();
        }
    }
}
