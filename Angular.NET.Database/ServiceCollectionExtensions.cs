using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Angular.NET.Database
{
    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// DI - add DB service
        /// </summary>
        /// <param name="services"></param>
        public static void AddDatabaseService(this IServiceCollection services)
        {
            services.TryAddSingleton<IDatabaseService, DatabaseService>();
            services.AddHostedService<DatabaseService>();
        }
    }
}
