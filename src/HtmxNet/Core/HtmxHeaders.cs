namespace HtmxNet.Core;

/// <summary>
/// Contains constants for all htmx HTTP headers used in requests and responses.
/// </summary>
public static class HtmxHeaders
{
    #region Request Headers

    /// <summary>
    /// Indicates that the request was made with htmx.
    /// </summary>
    public const string Request = "HX-Request";

    /// <summary>
    /// The ID of the target element if it exists.
    /// </summary>
    public const string Target = "HX-Target";

    /// <summary>
    /// The ID of the triggered element if it exists.
    /// </summary>
    public const string Trigger = "HX-Trigger";

    /// <summary>
    /// The name of the triggered element if it exists.
    /// </summary>
    public const string TriggerName = "HX-Trigger-Name";

    /// <summary>
    /// The current URL of the browser.
    /// </summary>
    public const string CurrentUrl = "HX-Current-URL";

    /// <summary>
    /// The user response to an hx-prompt.
    /// </summary>
    public const string Prompt = "HX-Prompt";

    /// <summary>
    /// Indicates that the request is via an element using hx-boost.
    /// </summary>
    public const string Boosted = "HX-Boosted";

    /// <summary>
    /// Indicates that the request is for history restoration after a miss in the local history cache.
    /// </summary>
    public const string HistoryRestoreRequest = "HX-History-Restore-Request";

    #endregion

    #region Response Headers

    /// <summary>
    /// Allows you to do a client-side redirect that does not do a full page reload.
    /// </summary>
    public const string Location = "HX-Location";

    /// <summary>
    /// Pushes a new url into the history stack.
    /// </summary>
    public const string PushUrl = "HX-Push-Url";

    /// <summary>
    /// Can be used to do a client-side redirect to a new location.
    /// </summary>
    public const string Redirect = "HX-Redirect";

    /// <summary>
    /// If set to "true" the client side will do a full refresh of the page.
    /// </summary>
    public const string Refresh = "HX-Refresh";

    /// <summary>
    /// Replaces the current URL in the location bar.
    /// </summary>
    public const string ReplaceUrl = "HX-Replace-Url";

    /// <summary>
    /// Allows you to specify how the response will be swapped.
    /// </summary>
    public const string Reswap = "HX-Reswap";

    /// <summary>
    /// A CSS selector that updates the target of the content update to a different element on the page.
    /// </summary>
    public const string Retarget = "HX-Retarget";

    /// <summary>
    /// A CSS selector that allows you to choose which part of the response is used to be swapped in.
    /// </summary>
    public const string Reselect = "HX-Reselect";

    /// <summary>
    /// Allows you to trigger client side events.
    /// </summary>
    public const string TriggerResponse = "HX-Trigger";

    /// <summary>
    /// Allows you to trigger client side events after the settle step.
    /// </summary>
    public const string TriggerAfterSettle = "HX-Trigger-After-Settle";

    /// <summary>
    /// Allows you to trigger client side events after the swap step.
    /// </summary>
    public const string TriggerAfterSwap = "HX-Trigger-After-Swap";

    #endregion
}