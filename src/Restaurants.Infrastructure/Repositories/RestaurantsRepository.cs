using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public RestaurantsRepository(ApplicationDBContext applicationDBContext) { 
            _dbContext = applicationDBContext;
        }
        public async Task<IReadOnlyList<Restaurant>> GetRestaurantsAsync(CancellationToken cancellationToken = default)
        {
            IReadOnlyList<Restaurant> restaurants =  await _dbContext.Restaurants.AsNoTracking().Include(r => r.Dishes).ToListAsync(cancellationToken);
            return restaurants;
        }

        public async Task<(int,IReadOnlyList<Restaurant>)> GetMatchingRestaurantsAsync(string? searchPhrase,int pageSize,int pageNumber,string? searchKey, SortDirection sortDirection, CancellationToken cancellationToken = default)
        {
            IQueryable<Restaurant> query =  _dbContext.Restaurants.Include(r => r.Dishes);
            if ( !string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = query.Where(r => r.Name.ToLower().Contains(searchPhrase) ||
                          r.Description.ToLower().Contains(searchPhrase));
            }
            if (searchKey != null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>()
                {
                    {nameof(Restaurant.Name),r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description},
                    {nameof(Restaurant.Category), r => r.Category }
                };
                var selectedColExpression = columnSelector[searchKey];
                query = sortDirection == SortDirection.Ascending ? query.OrderBy(selectedColExpression) : query.OrderByDescending(selectedColExpression);
            }
            var totalCount = await query.CountAsync();
            var restaurants = await query.Skip(pageSize * (pageNumber-1 )).Take(pageSize) 
                .ToListAsync(cancellationToken);
            return (totalCount,restaurants);
        }
        public async Task<Restaurant?> GetRestaurantByIdAsync(int id, CancellationToken cancellation = default)
        {
            Restaurant? restaurant = await _dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(r => r.Id == id, cancellation);
            return restaurant;
        }

        public async Task<int> CreateRestaurantAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            await _dbContext.Restaurants.AddAsync(restaurant, cancellationToken);
            await _dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<bool> DeleteRestaurant(int id,CancellationToken cancellationToken = default)
        {
            var restaurant = await GetRestaurantByIdAsync(id, cancellationToken);
            if ( restaurant == null) return false;
            _dbContext.Restaurants.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
