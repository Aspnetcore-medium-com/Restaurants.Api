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
    }
}
