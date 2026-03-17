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
        /// <summary>
        /// Retrieves a read-only list of all restaurants.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. If cancellation is requested, the operation will be aborted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of <see
        /// cref="RestaurantResponseDto"/> objects representing all restaurants. The list will be empty if no
        /// restaurants are available.</returns>
        Task<IReadOnlyList<RestaurantResponseDto>> GetAllRestaurants(CancellationToken cancellationToken = default);
        /// <summary>
        /// Retrieves the details of a restaurant by its unique identifier.
        /// </summary>
        /// <param name="restaurantId">The unique identifier of the restaurant to retrieve. Must be a positive integer.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. Optional.</param>
        /// <returns>A <see cref="RestaurantResponseDto"/> containing the restaurant's details if found; otherwise, <see
        /// langword="null"/>.</returns>
        Task<RestaurantResponseDto> GetRestaurantById(int restaurantId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates a restaurant and returns the id
        /// </summary>
        /// <param name="restaurantRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> Returns a <see cref="int"/> Restaurant id</returns>
        Task<int> CreateRestaurant(RestaurantRequestDto restaurantRequestDto, CancellationToken cancellationToken = default);
    }
}
