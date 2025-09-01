using Microsoft.AspNetCore.Mvc;

namespace HtmxNet.Abstractions;

/// <summary>
/// Provides a fluent API for building htmx-specific HTTP responses.
/// </summary>
public interface IHtmxResponseBuilder
{
    /// <summary>
    /// Triggers an event on the client side after the response is received.
    /// </summary>
    /// <param name="eventName">The name of the event to trigger.</param>
    /// <param name="data">Optional data to pass with the event.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder TriggerEvent(string eventName, object? data = null);

    /// <summary>
    /// Triggers an event on the client side after the settle step.
    /// </summary>
    /// <param name="eventName">The name of the event to trigger.</param>
    /// <param name="data">Optional data to pass with the event.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder TriggerEventAfterSettle(string eventName, object? data = null);

    /// <summary>
    /// Triggers an event on the client side after the swap step.
    /// </summary>
    /// <param name="eventName">The name of the event to trigger.</param>
    /// <param name="data">Optional data to pass with the event.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder TriggerEventAfterSwap(string eventName, object? data = null);

    /// <summary>
    /// Pushes a new URL into the browser's history stack.
    /// </summary>
    /// <param name="url">The URL to push to the history stack.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder PushUrl(string url);

    /// <summary>
    /// Replaces the current URL in the browser's history stack.
    /// </summary>
    /// <param name="url">The URL to replace in the history stack.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder ReplaceUrl(string url);

    /// <summary>
    /// Performs a client-side redirect to the specified URL.
    /// </summary>
    /// <param name="url">The URL to redirect to.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder Redirect(string url);

    /// <summary>
    /// Triggers a full page refresh on the client side.
    /// </summary>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder Refresh();

    /// <summary>
    /// Performs an out-of-band swap for the specified element.
    /// </summary>
    /// <param name="selector">The CSS selector for the target element.</param>
    /// <param name="content">The HTML content to swap.</param>
    /// <returns>The response builder for method chaining.</returns>
    IHtmxResponseBuilder SwapOob(string selector, string content);

    /// <summary>
    /// Builds and returns the configured htmx response.
    /// </summary>
    /// <returns>An action result containing the htmx response.</returns>
    IActionResult Build();
}