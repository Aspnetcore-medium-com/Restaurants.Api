using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Exceptions;


namespace Restaurants.Core.Users.Command.DeleteRole
{
    public class DeleteRoleCommandRequestHandler(ILogger<DeleteRoleCommandRequestHandler> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager) : IRequestHandler<DeleteRoleRequestCommand>
    {
        public async Task Handle(DeleteRoleRequestCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("delete role {@Request}", request);
            var user = await userManager.FindByEmailAsync(request.EmailId);
            if (user == null) { throw new NotFoundException(nameof(user), request.EmailId); }
            var role = await roleManager.FindByNameAsync(request.RoleName);
            if (role == null) { throw new NotFoundException(nameof(role), request.RoleName); }
            await userManager.RemoveFromRoleAsync(user, role.NormalizedName!);
              
        }
    }
}
