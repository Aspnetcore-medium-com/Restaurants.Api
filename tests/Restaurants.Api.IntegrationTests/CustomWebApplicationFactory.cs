using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Api.IntegrationTests
{
    // WebApplicationFactory<Program> spins up a real in-process instance of your API
    // using Program as the entry point, but allows us to override services before
    // the app starts — perfect for swapping out real dependencies with test doubles.
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        // A fixed database name ensures every test in the session hits the same
        // in-memory database instance. If we used a random Guid here, each factory
        // creation would produce an empty database with no seeded data.
        private const string TestDbName = "RestaurantsTestDb";

        // ConfigureWebHost is called by the framework before the app is built.
        // This is our hook to modify the DI container — removing real services
        // and replacing them with test-friendly alternatives.
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Tells the app to load appsettings.Testing.json instead of appsettings.json.
            // Our appsettings.Testing.json has an empty connection string for RestaurantsDB,
            // which causes AddInfra() to skip the UseSqlServer() registration entirely —
            // meaning there is no SQL Server provider to conflict with InMemory.
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Since SQL Server was never registered (due to empty connection string above),
                // we can safely add the InMemory provider without any provider conflict.
                // All repository and DbContext injections will now use this in-memory store.
                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase(TestDbName);
                });

                // Replaces the real authentication scheme (JWT/Identity) with our fake handler.
                // defaultScheme: "FakeScheme" means every request that needs authentication
                // will be routed to FakeAuthHandler.HandleAuthenticateAsync(), which always
                // returns a successful result with our predefined test claims.
                services.AddAuthentication(defaultScheme: "FakeScheme")
                    .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("FakeScheme", options => { });

                // Replaces the default authorization policy with one that:
                // 1. Only accepts identities authenticated by "FakeScheme"
                // 2. Requires the user to be authenticated (i.e. HandleAuthenticateAsync succeeded)
                // Without this, [Authorize] attributes would still challenge against the
                // original JWT scheme and return 401 even with our fake handler in place.
                services.AddAuthorizationBuilder()
                    .SetDefaultPolicy(new AuthorizationPolicyBuilder("FakeScheme")
                        .RequireAuthenticatedUser()
                        .Build());

                // Build a temporary service provider so we can resolve services
                // at configuration time — specifically to access the DbContext for seeding.
                // This is separate from the final app service provider.
                var sp = services.BuildServiceProvider();

                // CreateScope() is required because DbContext is registered as Scoped —
                // it cannot be resolved directly from the root provider, only from a scope.
                using var scope = sp.CreateScope();

                // Resolve our InMemory DbContext instance from the scoped provider.
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                // EnsureCreated() sets up the database schema based on our entity model.
                // For InMemory this is lightweight — it just initialises the store.
                // Note: we never call Migrate() here as that is relational-only.
                db.Database.EnsureCreated();

                // Add a known restaurant with a fixed Id so our tests can make
                // deterministic requests like GET /api/restaurants/1 and expect it to exist.
                db.Restaurants.Add(new Restaurant
                {
                    Name = "Spice House",
                    Description = "Indian food",
                    Category = "Indian",
                    Id = 1
                });
                db.SaveChanges();
            });
        }
    }
}
