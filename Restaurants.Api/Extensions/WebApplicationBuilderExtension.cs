using Microsoft.OpenApi;
using Serilog;

namespace Restaurants.Api.Extensions
{
    public static class WebApplicationBuilderExtension
    {

        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddHttpLogging(options =>
            {

            });

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);

            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Enter JWT token like: Bearer {token}"
                });
                c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("bearerAuth"),
            []
        }
    });
            });
        }
    }
}
