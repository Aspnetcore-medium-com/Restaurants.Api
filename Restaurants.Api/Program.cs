
using Restaurants.Api.Seeders;
using Restaurants.Core.Extension;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfra(builder.Configuration).AddCore();

var app = builder.Build();

await app.Seed();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
