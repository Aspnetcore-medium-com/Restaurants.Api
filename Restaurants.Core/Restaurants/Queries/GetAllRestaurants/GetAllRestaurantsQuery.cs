using MediatR;
using Restaurants.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery:IRequest<PagedResult<RestaurantResponseDto>>
    {
        public string? searchPhrase { get; set; } = string.Empty;
        public int  pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
