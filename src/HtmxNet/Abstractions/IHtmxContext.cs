namespace HtmxNet.Abstractions;

/// <summary>
/// Provides access to htmx-specific request information from HTTP headers.
/// </summary>
public interface IHtmxContext
{
    /// <summary>
    /// Gets a value indicating whether the current request was made by htmx.
    /// </summary>
    bool IsHtmxRequest { get; }

    /// <summary>
    /// Gets the ID of the target element that htmx will swap content into.
    /// </summary>
    string? Target { get; }

    /// <summary>
    /// Gets the ID of the element that triggered the htmx request.
    /// </summary>
    string? Trigger { get; }

    /// <summary>
    /// Gets the name attribute of the element that triggered the htmx request.
    /// </summary>
    string? TriggerName { get; }

    /// <summary>
    /// Gets the current URL of the page when the htmx request was made.
    /// </summary>
    string? CurrentUrl { get; }

    /// <summary>
    /// Gets the response to any hx-prompt on the triggering element.
    /// </summary>
    string? PromptResponse { get; }

    /// <summary>
    /// Gets a value indicating whether the request is via an element using hx-boost.
    /// </summary>
    bool IsBoosted { get; }

    /// <summary>
    /// Gets a value indicating whether the request is for history restoration after a miss in the local history cache.
    /// </summary>
    bool IsHistoryRestoreRequest { get; }

    /// <summary>
    /// Gets a dictionary of custom htmx headers that were sent with the request.
    /// </summary>
    IDictionary<string, string> CustomHeaders { get; }
}