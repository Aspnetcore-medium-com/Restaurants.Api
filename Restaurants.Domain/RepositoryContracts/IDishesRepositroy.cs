using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.RepositoryContracts
{
    public interface IDishesRepositroy
    {
        /// <summary>
        /// Asynchronously creates a new dish in the data store.
        /// </summary>
        /// <param name="dish">The <see cref="Dish"/> object containing the details of the dish to create. Cannot be <c>null</c>.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateDishAsync(Dish dish, CancellationToken cancellationToken = default);
        /// <summary>
        /// Ascynchronously deletes dishes 
        /// </summary>
        /// <param name="dishes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteDishesAsync(List<Dish> dishes, CancellationToken cancellationToken = default);
        /// <summary>
        /// Asynchronously checks the exists of Dishes
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>bool</returns>
        Task<bool> HasDishesAsync(int restaurantId, CancellationToken cancellationToken = default);
    }
}
