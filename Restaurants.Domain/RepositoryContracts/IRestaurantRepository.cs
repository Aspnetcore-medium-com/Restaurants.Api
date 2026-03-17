using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.RepositoryContracts
{
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Retrieves a read-only list of all available restaurants.
        /// </summary>
        /// <param name="cancellationToken">A token that can be used to cancel the operation. If cancellation is requested, the task will complete
        /// early.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of <see
        /// cref="Restaurant"/> objects representing the available restaurants. The list will be empty if no restaurants
        /// are found.</returns>
        Task<IReadOnlyList<Restaurant>> GetRestaurantsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves a restaurant by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant to retrieve. Must be a positive integer.</param>
        /// <param name="cancellation">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant with the
        /// specified identifier, or null if no restaurant is found.</returns>
        Task<Restaurant?> GetRestaurantByIdAsync(int id, CancellationToken cancellation = default);
        /// <summary>
        /// Creates a new restaurant entry asynchronously.
        /// </summary>
        /// <param name="restaurant">The <see cref="Restaurant"/> object containing the details of the restaurant to create. Cannot be null.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. Optional.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifier of the
        /// newly created restaurant.</returns>
        Task<int> CreateRestaurantAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
    }
}
