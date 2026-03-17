using Restaurants.Core.Dtos.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.ServiceContracts
{
    public interface IRestaurantsService
    {
        Task<IReadOnlyList<RestaurantResponseDto>> GetAllRestaurants(CancellationToken cancellationToken = default);
    }
}
