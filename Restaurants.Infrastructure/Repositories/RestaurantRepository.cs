using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RestaurantRepository(ApplicationDBContext applicationDBContext) { 
            _dbContext = applicationDBContext;
        }
        public async Task<IReadOnlyList<Restaurant>> GetRestaurants()
        {
            IReadOnlyList<Restaurant> restaurants =  await _dbContext.Restaurants.AsNoTracking().ToListAsync();
            return restaurants;
        }
    }
}
