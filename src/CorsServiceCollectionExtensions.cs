using System;
using Byndyusoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up cross-origin resource sharing services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CorsServiceCollectionExtensions
    {
        /// <summary>
        /// Adds cross-origin resource sharing services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddInsecureCors(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCors();

            services.Add(ServiceDescriptor.Transient<ICorsService, InsecuredCorsService>());
 
            return services;
        }

        /// <summary>
        /// Adds cross-origin resource sharing services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="setupAction">An <see cref="Action{CorsOptions}"/> to configure the provided <see cref="CorsOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddInsecureCors(this IServiceCollection services, Action<CorsOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddInsecureCors();
            services.Configure(setupAction);

            return services;
        }
    }
}