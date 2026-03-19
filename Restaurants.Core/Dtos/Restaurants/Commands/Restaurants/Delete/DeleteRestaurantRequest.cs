using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Commands.Restaurants.Delete
{
    public class DeleteRestaurantRequest(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
}
