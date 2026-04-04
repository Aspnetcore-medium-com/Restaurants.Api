using MediatR;

namespace Restaurants.Core.Users.Command.AssignRole
{
    public class AssignRoleCommand : IRequest
    {
        public string Email { get; set; } = default!;
        public string RoleName { get; set; } = default!;

    }
}
