using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("RestaurantsDB")).EnableSensitiveDataLogging());
            services.AddScoped<IRestaurantsRepository,RestaurantsRepository>();
            services.AddScoped<IDishesRepositroy,DishesRepository>();
            services.AddIdentityApiEndpoints<ApplicationUser>()
                        .AddEntityFrameworkStores<ApplicationDBContext>();
            return services;
        }
    }
}
