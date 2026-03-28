

using MediatR;

namespace Restaurants.Core.Users.Command.DeleteRole
{
    public class DeleteRoleRequestCommand : IRequest
    {
        public string EmailId { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
