
using Restaurants.Api.Seeders;
using Restaurants.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfra(builder.Configuration);

var app = builder.Build();

await app.Seed();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
