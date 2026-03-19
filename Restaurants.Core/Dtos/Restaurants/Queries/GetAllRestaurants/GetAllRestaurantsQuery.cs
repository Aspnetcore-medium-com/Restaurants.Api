using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery:IRequest<IReadOnlyList<RestaurantResponseDto>>
    {
    }
}
