using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Core.Users.Command;

namespace Restaurants.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            await mediator.Send(updateUserCommand);
            return NoContent();
        }

    }
}
