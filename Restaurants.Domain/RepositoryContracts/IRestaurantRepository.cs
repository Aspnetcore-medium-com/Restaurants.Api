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
        Task<IReadOnlyList<Restaurant>> GetRestaurants();
    }
}
