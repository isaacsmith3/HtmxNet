using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using HtmxNet.Abstractions;
using HtmxNet.Core;

namespace HtmxNet.Middleware;

/// <summary>
/// Extension methods for registering htmx middleware and services.
/// </summary>
public static class HtmxMiddlewareExtensions
{
    /// <summary>
    /// Adds htmx services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services is null.</exception>
    public static IServiceCollection AddHtmx(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        // Register the HtmxContext as scoped service
        services.AddScoped<IHtmxContext>(provider =>
        {
            var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor?.HttpContext != null)
            {
                // Try to get the context from HttpContext.Items first
                var existingContext = HtmxMiddleware.GetHtmxContext(httpContextAccessor.HttpContext);
                if (existingContext != null)
                {
                    return existingContext;
                }
                
                // If not found, create a new one
                return new HtmxContext(httpContextAccessor.HttpContext);
            }
            
            throw new InvalidOperationException("HtmxContext requires an active HttpContext. Ensure the middleware is registered and the service is accessed within a request scope.");
        });

        // Register HttpContextAccessor if not already registered
        services.AddHttpContextAccessor();

        return services;
    }

    /// <summary>
    /// Adds htmx services to the service collection with configuration options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure htmx options.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when services is null.</exception>
    public static IServiceCollection AddHtmx(this IServiceCollection services, Action<HtmxOptions> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions));

        // Configure options
        services.Configure(configureOptions);
        
        // Add base htmx services
        return services.AddHtmx();
    }

    /// <summary>
    /// Adds the htmx middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when app is null.</exception>
    public static IApplicationBuilder UseHtmx(this IApplicationBuilder app)
    {
        if (app == null)
            throw new ArgumentNullException(nameof(app));

        return app.UseMiddleware<HtmxMiddleware>();
    }
}

/// <summary>
/// Configuration options for htmx integration.
/// </summary>
public class HtmxOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to automatically detect and return partial views for htmx requests.
    /// </summary>
    public bool AutoDetectPartialViews { get; set; } = HtmxConstants.Defaults.AutoDetectPartialViews;

    /// <summary>
    /// Gets or sets the suffix used to identify partial view files.
    /// </summary>
    public string PartialViewSuffix { get; set; } = HtmxConstants.Defaults.PartialViewSuffix;

    /// <summary>
    /// Gets or sets a value indicating whether to include htmx request headers in the context.
    /// </summary>
    public bool IncludeRequestHeaders { get; set; } = HtmxConstants.Defaults.IncludeRequestHeaders;

    /// <summary>
    /// Gets or sets a value indicating whether to enable detailed error messages in development mode.
    /// </summary>
    public bool EnableDetailedErrors { get; set; } = HtmxConstants.Defaults.EnableDetailedErrors;

    /// <summary>
    /// Gets or sets the default headers to include in htmx responses.
    /// </summary>
    public Dictionary<string, string> DefaultHeaders { get; set; } = new();

    /// <summary>
    /// Gets or sets the custom header patterns that should be included in the htmx context.
    /// </summary>
    public List<string> CustomHeaderPatterns { get; set; } = new();
}