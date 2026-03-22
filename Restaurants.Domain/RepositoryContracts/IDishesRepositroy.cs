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
    }
}
