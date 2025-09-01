namespace HtmxNet.Core;

/// <summary>
/// Contains constants for common htmx attribute values and configuration.
/// </summary>
public static class HtmxConstants
{
    #region Swap Styles

    /// <summary>
    /// Common htmx swap styles used in hx-swap attributes.
    /// </summary>
    public static class SwapStyles
    {
        /// <summary>
        /// Replace the inner html of the target element.
        /// </summary>
        public const string InnerHtml = "innerHTML";

        /// <summary>
        /// Replace the entire target element with the response.
        /// </summary>
        public const string OuterHtml = "outerHTML";

        /// <summary>
        /// Insert the response before the target element.
        /// </summary>
        public const string BeforeBegin = "beforebegin";

        /// <summary>
        /// Insert the response before the first child of the target element.
        /// </summary>
        public const string AfterBegin = "afterbegin";

        /// <summary>
        /// Insert the response after the last child of the target element.
        /// </summary>
        public const string BeforeEnd = "beforeend";

        /// <summary>
        /// Insert the response after the target element.
        /// </summary>
        public const string AfterEnd = "afterend";

        /// <summary>
        /// Deletes the target element regardless of the response.
        /// </summary>
        public const string Delete = "delete";

        /// <summary>
        /// Does not append content from response (out of band items will still be processed).
        /// </summary>
        public const string None = "none";
    }

    #endregion

    #region HTTP Methods

    /// <summary>
    /// HTTP methods commonly used with htmx attributes.
    /// </summary>
    public static class HttpMethods
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Patch = "PATCH";
        public const string Delete = "DELETE";
    }

    #endregion

    #region Trigger Events

    /// <summary>
    /// Common trigger events used in hx-trigger attributes.
    /// </summary>
    public static class TriggerEvents
    {
        public const string Click = "click";
        public const string Change = "change";
        public const string KeyUp = "keyup";
        public const string Submit = "submit";
        public const string Load = "load";
        public const string Revealed = "revealed";
        public const string Intersect = "intersect";
    }

    #endregion

    #region Configuration Defaults

    /// <summary>
    /// Default configuration values for the htmx library.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Default suffix for partial view files.
        /// </summary>
        public const string PartialViewSuffix = "_Partial";

        /// <summary>
        /// Default value for auto-detecting partial views.
        /// </summary>
        public const bool AutoDetectPartialViews = true;

        /// <summary>
        /// Default value for including request headers.
        /// </summary>
        public const bool IncludeRequestHeaders = true;

        /// <summary>
        /// Default value for enabling detailed errors.
        /// </summary>
        public const bool EnableDetailedErrors = false;
    }

    #endregion

    #region Content Types

    /// <summary>
    /// Content types commonly used in htmx responses.
    /// </summary>
    public static class ContentTypes
    {
        public const string Html = "text/html";
        public const string Json = "application/json";
        public const string PlainText = "text/plain";
    }

    #endregion
}