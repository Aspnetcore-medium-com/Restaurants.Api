
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Api.Seeders;
using Restaurants.Core.Extension;
using Restaurants.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfra(builder.Configuration).AddCore();
builder.Services.AddHttpLogging( options =>
{

});

var app = builder.Build();

await app.Seed();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseHttpLogging();

app.MapControllers();

app.Run();
