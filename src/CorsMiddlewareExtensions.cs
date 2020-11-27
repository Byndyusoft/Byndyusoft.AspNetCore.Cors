using System;
using Byndyusoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// The <see cref="IApplicationBuilder"/> extensions for adding CORS middleware support.
    /// </summary>
    public static class CorsMiddlewareExtensions
    {
        /// <summary>
        /// Adds a CORS middleware to your web application pipeline to allow cross domain requests.
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method</param>
        /// <returns>The original app parameter</returns>
        /// <remarks>Allows <b>AllowsAnyOrigin</b> and <b>AllowsCredentials</b> policy options to be used together.</remarks>
        public static IApplicationBuilder UseInsecureCors(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            CorsPolicyBuilderPatcher.Patch();

            return app.UseCors();
        }

        /// <summary>
        /// Adds a CORS middleware to your web application pipeline to allow cross domain requests.
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method</param>
        /// <param name="policyName">The policy name of a configured policy.</param>
        /// <returns>The original app parameter</returns>
        /// <remarks>Allows <b>AllowsAnyOrigin</b> and <b>AllowsCredentials</b> policy options to be used together.</remarks>
        public static IApplicationBuilder UseInsecureCors(this IApplicationBuilder app, string policyName)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            CorsPolicyBuilderPatcher.Patch();

            return app.UseCors(policyName);
        }

        /// <summary>
        /// Adds a CORS middleware to your web application pipeline to allow cross domain requests.
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method.</param>
        /// <param name="configurePolicy">A delegate which can use a policy builder to build a policy.</param>
        /// <returns>The original app parameter</returns>
        /// <remarks>Allows <b>AllowsAnyOrigin</b> and <b>AllowsCredentials</b> policy options to be used together.</remarks>
        public static IApplicationBuilder UseInsecureCors(
            this IApplicationBuilder app,
            Action<CorsPolicyBuilder> configurePolicy)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configurePolicy == null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            CorsPolicyBuilderPatcher.Patch();

            return app.UseCors(configurePolicy);
        }
    }
}