
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Api.Middlewares;
using Restaurants.Api.Seeders;
using Restaurants.Core.Extension;
using Restaurants.Domain.Entities;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseErrorHandlingMiddleware();
await app.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.MapIdentityApi<ApplicationUser>();
app.UseAuthorization();
app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Run();
