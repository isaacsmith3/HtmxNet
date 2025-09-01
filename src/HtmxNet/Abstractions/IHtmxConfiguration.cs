namespace HtmxNet.Abstractions;

/// <summary>
/// Provides configuration options for htmx integration.
/// </summary>
public interface IHtmxConfiguration
{
    /// <summary>
    /// Gets a value indicating whether to automatically detect and return partial views for htmx requests.
    /// </summary>
    bool AutoDetectPartialViews { get; }

    /// <summary>
    /// Gets the suffix used to identify partial view files.
    /// </summary>
    string PartialViewSuffix { get; }

    /// <summary>
    /// Gets a value indicating whether to include htmx request headers in the context.
    /// </summary>
    bool IncludeRequestHeaders { get; }

    /// <summary>
    /// Gets a value indicating whether to enable detailed error messages in development mode.
    /// </summary>
    bool EnableDetailedErrors { get; }

    /// <summary>
    /// Gets the default headers to include in htmx responses.
    /// </summary>
    IDictionary<string, string> DefaultHeaders { get; }

    /// <summary>
    /// Gets the custom header patterns that should be included in the htmx context.
    /// </summary>
    IEnumerable<string> CustomHeaderPatterns { get; }
}