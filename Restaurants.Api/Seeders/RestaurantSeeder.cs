using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurants.Api.Seeders
{
    public static class RestaurantSeeder
    {
        public static async Task Seed(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            await context.Database.MigrateAsync();

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
        }
    }
}
