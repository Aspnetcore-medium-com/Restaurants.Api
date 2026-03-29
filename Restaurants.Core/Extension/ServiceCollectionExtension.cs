using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Core.MappingProfiles;
using Restaurants.Core.ServiceContracts;
using Restaurants.Core.Services;
using Restaurants.Core.Users.User;
using Restaurants.Core.Validators;
using Restaurants.Domain.Interfaces;
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
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
