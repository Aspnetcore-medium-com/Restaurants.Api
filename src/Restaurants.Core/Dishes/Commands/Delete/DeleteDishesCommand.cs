using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dishes.Commands.Delete
{
    public class DeleteDishesCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
