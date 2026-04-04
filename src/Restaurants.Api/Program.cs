using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Restaurants.Api.Extensions;
using Restaurants.Api.Middlewares;
using Restaurants.Api.Seeders;
using Restaurants.Core.Extension;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentation();
builder.Services.AddInfra(builder.Configuration).AddCore();

var app = builder.Build();

app.UseErrorHandlingMiddleware();
await app.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.MapGroup("api/identity").MapIdentityApi<ApplicationUser>().AllowAnonymous();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.MapControllers().RequireAuthorization(); 

app.Run();
public partial class Program { }