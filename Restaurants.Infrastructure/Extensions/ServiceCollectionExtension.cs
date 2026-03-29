using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.RepositoryContracts;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.AgeRequirement;
using Restaurants.Infrastructure.Authorization.Services;
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
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                            RestaurantsUserClaimsPrincipalFactory>();
            services.AddIdentityApiEndpoints<ApplicationUser>()
                        .AddRoles<ApplicationRole>()
                        .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                        .AddEntityFrameworkStores<ApplicationDBContext>();
            services.AddScoped<IAuthorizationHandler,MinimumAgeRequirementHandler>();
            services.AddAuthorizationBuilder()
                    .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality,"British","Indian"))
                    .AddPolicy(PolicyNames.AgePolicy, builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

            return services;
        }
    }
}
