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
    public class DishesRepository(ApplicationDBContext applicationDBContext) : IDishesRepositroy
    {
        public async Task CreateDishAsync(Dish dish,CancellationToken cancellationToken = default)
        {
            await applicationDBContext.Dishes.AddAsync(dish, cancellationToken);   
            await applicationDBContext.SaveChangesAsync();
        }
    }
}
