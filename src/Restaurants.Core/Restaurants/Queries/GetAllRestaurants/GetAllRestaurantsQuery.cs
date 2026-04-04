using MediatR;
using Restaurants.Core.Common;
using Restaurants.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Dtos.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantResponseDto>>
    {
        public string? SearchPhrase { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string? SortKey {get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
