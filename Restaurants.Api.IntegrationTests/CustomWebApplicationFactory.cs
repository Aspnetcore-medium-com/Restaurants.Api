using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Api.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // SQL Server was never registered (empty connection string), 
                // so just add InMemory cleanly
                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("RestaurantsTestDb_" + Guid.NewGuid());
                });
                // Replace authentication with fake scheme
                services.AddAuthentication("FakeScheme")
                    .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("FakeScheme", options => { });
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                db.Database.EnsureCreated();
                db.Restaurants.Add(new Restaurant
                {
                    Name = "Spice House",
                    Description = "Indian food",
                    Category = "Indian"
                });
                db.SaveChanges();
            });
        }
    }
}