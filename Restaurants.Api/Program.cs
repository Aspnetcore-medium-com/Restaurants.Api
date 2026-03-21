
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Api.Seeders;
using Restaurants.Core.Extension;
using Restaurants.Infrastructure.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfra(builder.Configuration).AddCore();
builder.Services.AddHttpLogging( options =>
{

});

builder.Host.UseSerilog((context,services,configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
        
});

var app = builder.Build();

await app.Seed();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
