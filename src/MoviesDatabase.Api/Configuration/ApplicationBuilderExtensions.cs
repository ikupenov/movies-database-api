using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesDatabase.Api.Configuration.Settings;
using MoviesDatabase.Api.Middlewares;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Infrastructure.Data.Prototyping;

namespace MoviesDatabase.Api.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedFakeData(this IApplicationBuilder @this)
        {
            using (var scope = @this.ApplicationServices.CreateScope())
            {
                var manager = scope.ServiceProvider.GetService<IManager>();
                var dataSeeder = new DataSeeder(manager);

                dataSeeder.Seed();
            }

            return @this;
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<GlobalExceptionMiddleware>();

            return @this;
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder @this, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSettings<SwaggerSettings>();

            @this.UseSwagger();

            @this.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerSettings.Endpoint, swaggerSettings.Title);
                options.RoutePrefix = string.Empty;
            });

            return @this;
        }
    }
}
