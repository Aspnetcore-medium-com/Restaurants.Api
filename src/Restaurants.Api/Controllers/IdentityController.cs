using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Core.Users.Command.AssignRole;
using Restaurants.Core.Users.Command.DeleteRole;
using Restaurants.Core.Users.Command.Update;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

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

        [HttpPost("userrole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleCommand assignRoleCommand)
        {
            await mediator.Send(assignRoleCommand);
            return NoContent();
        }

        [HttpDelete("userrole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteRoleFromUser(DeleteRoleRequestCommand deleteRoleCommand)
        {
            await mediator.Send(deleteRoleCommand);
            return NoContent();
        }

    }
}
