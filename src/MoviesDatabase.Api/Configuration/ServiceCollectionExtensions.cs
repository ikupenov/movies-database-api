using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesDatabase.Api.Configuration.Settings;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Core.Logging;
using MoviesDatabase.Core.Modules.Movies;
using MoviesDatabase.Core.Modules.Users;
using MoviesDatabase.Infrastructure.Data;
using MoviesDatabase.Infrastructure.Gateways;

namespace MoviesDatabase.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection @this, IConfiguration configuration)
        {
            @this
                .AddDatabase(configuration)
                .AddGateways()
                .AddManagers()
                .AddLogging()
                .AddAutoMapper()
                .AddSwagger(configuration);

            return @this;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection @this, IConfiguration configuration)
        {
            var databaseName = configuration.GetSettings<DatabaseSettings>().DatabaseName;
            @this.AddDbContext<EntityFrameworkDbContext>(o => o.UseInMemoryDatabase(databaseName), ServiceLifetime.Scoped);

            return @this;
        }

        public static IServiceCollection AddGateways(this IServiceCollection @this)
        {
            @this.AddTransient<IManager, EntityFrameworkManager>();
            @this.AddTransient(typeof(IProvider<>), typeof(EntityFrameworkProvider<>));

            return @this;
        }

        public static IServiceCollection AddManagers(this IServiceCollection @this)
        {
            @this.AddScoped<IUserManager, UserManager>();
            @this.AddScoped<IMovieManager, MovieManager>();

            return @this;
        }

        public static IServiceCollection AddLogging(this IServiceCollection @this)
        {
            @this.AddSingleton<ILogger, ConsoleLogger>();

            return @this;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection @this, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSettings<SwaggerSettings>();

            @this.AddSwaggerGen(options =>
            {
                var info = new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = swaggerSettings.Title,
                    Version = swaggerSettings.Version
                };

                options.SwaggerDoc(swaggerSettings.Version, info);
            });

            return @this;
        }
    }
}
