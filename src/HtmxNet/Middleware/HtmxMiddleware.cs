using Microsoft.AspNetCore.Http;
using HtmxNet.Abstractions;
using HtmxNet.Core;

namespace HtmxNet.Middleware;

/// <summary>
/// Middleware that processes htmx-specific HTTP headers and populates the HtmxContext.
/// </summary>
public class HtmxMiddleware
{
    private readonly RequestDelegate _next;
    private const string HtmxContextKey = "HtmxContext";

    /// <summary>
    /// Initializes a new instance of the <see cref="HtmxMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <exception cref="ArgumentNullException">Thrown when next is null.</exception>
    public HtmxMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    /// <summary>
    /// Processes the HTTP request and populates the HtmxContext.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        // Create and populate the HtmxContext
        var htmxContext = new HtmxContext(context);
        
        // Store the context in HttpContext.Items for downstream access
        context.Items[HtmxContextKey] = htmxContext;
        
        // The context is now available through HttpContext.Items and DI

        // Continue to the next middleware
        await _next(context);
    }

    /// <summary>
    /// Gets the HtmxContext from the current HttpContext.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>The HtmxContext if available, otherwise null.</returns>
    public static IHtmxContext? GetHtmxContext(HttpContext httpContext)
    {
        if (httpContext?.Items.TryGetValue(HtmxContextKey, out var context) == true)
        {
            return context as IHtmxContext;
        }
        
        return null;
    }
}