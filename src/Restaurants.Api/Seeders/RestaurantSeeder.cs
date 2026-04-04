using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Restaurants.Api.Seeders
{
    public static class RestaurantSeeder
    {
        public static async Task Seed(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            // Guard the relational-only call
            if (context.Database.IsRelational())
            {
                var dbName = context.Database.GetDbConnection().Database;
                if (string.IsNullOrWhiteSpace(dbName) || dbName == "master")
                {
                    return;
                }

                await context.Database.MigrateAsync();
                if (string.IsNullOrWhiteSpace(dbName) || dbName == "master")
                {
                    throw new Exception($"Invalid database detected: {dbName}");
                }
            }
            

            if (!await context.Restaurants.AnyAsync())
            {
                var jsonPath = Path.Combine(AppContext.BaseDirectory, "Seeders", "RestaurantSeeder.json");
                if (!File.Exists(jsonPath)) return;
                var json = await File.ReadAllTextAsync(jsonPath);
                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (restaurants == null || restaurants.Count == 0) return;
                await context.Restaurants.AddRangeAsync(restaurants);
                await context.SaveChangesAsync();
            }

            if (!await context.Roles.AnyAsync())
            {
                var roles = GetRoles();
                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<ApplicationRole> GetRoles()
        {
            List<ApplicationRole> roles =
            [
                new ApplicationRole
                {
                    Name = UserRoles.Owner,
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new ApplicationRole
                {
                    Name = UserRoles.Admin,
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new ApplicationRole
                {
                    Name = UserRoles.User,
                    NormalizedName = UserRoles.User.ToUpper()
                }
            ];

            return roles;
        }
    }
}
