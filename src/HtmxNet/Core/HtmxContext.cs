using Microsoft.AspNetCore.Http;
using HtmxNet.Abstractions;

namespace HtmxNet.Core;

/// <summary>
/// Provides access to htmx-specific request information from HTTP headers.
/// </summary>
public class HtmxContext : IHtmxContext
{
    private readonly IHeaderDictionary _headers;
    private readonly Dictionary<string, string> _customHeaders;

    /// <summary>
    /// Initializes a new instance of the <see cref="HtmxContext"/> class.
    /// </summary>
    /// <param name="httpContext">The HTTP context containing the request headers.</param>
    /// <exception cref="ArgumentNullException">Thrown when httpContext is null.</exception>
    public HtmxContext(HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));
        
        _headers = httpContext.Request.Headers;
        _customHeaders = BuildCustomHeaders();
    }

    /// <inheritdoc />
    public bool IsHtmxRequest => GetBooleanHeader(HtmxHeaders.Request);

    /// <inheritdoc />
    public string? Target => GetStringHeader(HtmxHeaders.Target);

    /// <inheritdoc />
    public string? Trigger => GetStringHeader(HtmxHeaders.Trigger);

    /// <inheritdoc />
    public string? TriggerName => GetStringHeader(HtmxHeaders.TriggerName);

    /// <inheritdoc />
    public string? CurrentUrl => GetStringHeader(HtmxHeaders.CurrentUrl);

    /// <inheritdoc />
    public string? PromptResponse => GetStringHeader(HtmxHeaders.Prompt);

    /// <inheritdoc />
    public bool IsBoosted => GetBooleanHeader(HtmxHeaders.Boosted);

    /// <inheritdoc />
    public bool IsHistoryRestoreRequest => GetBooleanHeader(HtmxHeaders.HistoryRestoreRequest);

    /// <inheritdoc />
    public IDictionary<string, string> CustomHeaders => _customHeaders;

    /// <summary>
    /// Gets a string header value from the request headers.
    /// </summary>
    /// <param name="headerName">The name of the header to retrieve.</param>
    /// <returns>The header value or null if not present.</returns>
    private string? GetStringHeader(string headerName)
    {
        if (_headers.TryGetValue(headerName, out var values))
        {
            var value = values.FirstOrDefault();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
        
        return null;
    }

    /// <summary>
    /// Gets a boolean header value from the request headers.
    /// </summary>
    /// <param name="headerName">The name of the header to retrieve.</param>
    /// <returns>True if the header is present and has a truthy value, false otherwise.</returns>
    private bool GetBooleanHeader(string headerName)
    {
        var value = GetStringHeader(headerName);
        
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Builds a dictionary of custom htmx headers that were sent with the request.
    /// </summary>
    /// <returns>A dictionary containing custom htmx headers.</returns>
    private Dictionary<string, string> BuildCustomHeaders()
    {
        var customHeaders = new Dictionary<string, string>();
        
        // Standard htmx headers that should not be included in custom headers
        var standardHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            HtmxHeaders.Request,
            HtmxHeaders.Target,
            HtmxHeaders.Trigger,
            HtmxHeaders.TriggerName,
            HtmxHeaders.CurrentUrl,
            HtmxHeaders.Prompt,
            HtmxHeaders.Boosted,
            HtmxHeaders.HistoryRestoreRequest
        };

        foreach (var header in _headers)
        {
            if (header.Key.StartsWith("HX-", StringComparison.OrdinalIgnoreCase) &&
                !standardHeaders.Contains(header.Key))
            {
                var value = header.Value.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    customHeaders[header.Key] = value;
                }
            }
        }

        return customHeaders;
    }
}