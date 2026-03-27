using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Users.Command.Update
{
    public class UpdateUserCommand : IRequest
    {
        public DateOnly? DateOnly { get; set; }
        public string? Nationality { get; set; }
    }
}
