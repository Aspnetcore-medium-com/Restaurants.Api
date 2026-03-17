using Microsoft.Extensions.DependencyInjection;
using Restaurants.Core.MappingProfiles;
using Restaurants.Core.ServiceContracts;
using Restaurants.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Core.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<IRestaurantsService, RestaurantsService>();
            services.AddAutoMapper(config =>
            {
                config.AddMaps(typeof(RestaurantProfile).Assembly);
            });
            return services;
        }
    }
}
