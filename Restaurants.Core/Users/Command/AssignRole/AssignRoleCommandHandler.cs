using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Exceptions;

namespace Restaurants.Core.Users.Command.AssignRole
{
    public class AssignRoleCommandHandler(ILogger<AssignRoleCommandHandler> logger, 
        UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : IRequestHandler<AssignRoleCommand>
    {
        public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assign role {@Request}", request);
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null) { throw new NotFoundException( nameof(user), request.Email) ; }
            var role = await roleManager.FindByNameAsync(request.RoleName);
            if (role == null) { throw new NotFoundException(nameof(role), request.RoleName); }
            await userManager.AddToRoleAsync(user, role.NormalizedName!);
        }
    }
}
