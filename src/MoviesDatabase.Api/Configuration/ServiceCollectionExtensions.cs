using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesDatabase.Api.Configuration.Settings;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Core.Logging;
using MoviesDatabase.Infrastructure.Data;
using MoviesDatabase.Infrastructure.Gateways;

namespace MoviesDatabase.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection @this)
        {
            @this.AddLogging();

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
            return @this;
        }

        private static IServiceCollection AddLogging(this IServiceCollection @this)
        {
            @this.AddSingleton<ILogger, ConsoleLogger>();

            return @this;
        }
    }
}
