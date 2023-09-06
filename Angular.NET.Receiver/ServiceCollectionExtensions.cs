using Angular.NET.Receiver;
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
        /// Add receiver service (TCP listener)
        /// </summary>
        /// <param name="services"></param>
        public static void AddReceiverService(this IServiceCollection services)
        {
            services.TryAddSingleton<IReceiverService, ReceiverService>();
            services.AddHostedService<ReceiverService>();
        }
    }
}
