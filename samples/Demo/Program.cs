using HtmxNet.Middleware;
using HtmxNet.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add htmx services
builder.Services.AddHtmx();

var app = builder.Build();

// Use htmx middleware
app.UseHtmx();

// Home page - shows side-by-side comparison of regular vs htmx requests
app.MapGet("/", (IHtmxContext htmxContext) =>
{
    var requestType = htmxContext.IsHtmxRequest ? "htmx-request" : "regular-request";
    var requestTitle = htmxContext.IsHtmxRequest ? "HTMX Request Detected!" : "Regular HTTP Request";

    var html = $@"<!DOCTYPE html>
<html>
<head>
    <title>HtmxNet Middleware Demo - Side by Side Comparison</title>
    <script src=""https://unpkg.com/htmx.org@1.9.10""></script>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 20px; background: #f8f9fa; }}
        .container {{ max-width: 1200px; margin: 0 auto; }}
        .header {{ text-align: center; margin-bottom: 30px; }}
        .comparison-container {{ display: flex; gap: 20px; margin-bottom: 30px; }}
        .section {{ flex: 1; background: white; border-radius: 8px; padding: 20px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
        .regular-section {{ border-left: 5px solid #dc3545; }}
        .htmx-section {{ border-left: 5px solid #28a745; }}
        .section h3 {{ margin-top: 0; color: #333; }}
        .regular-section h3 {{ color: #dc3545; }}
        .htmx-section h3 {{ color: #28a745; }}
        .info-box {{ background: #f5f5f5; padding: 15px; margin: 15px 0; border-radius: 5px; font-size: 14px; }}
        .htmx-request {{ background: #e8f5e8; border-left: 3px solid #28a745; }}
        .regular-request {{ background: #fff3cd; border-left: 3px solid #ffc107; }}
        button {{ padding: 12px 20px; margin: 10px 5px; cursor: pointer; border: none; border-radius: 4px; font-weight: bold; }}
        .regular-btn {{ background: #dc3545; color: white; }}
        .regular-btn:hover {{ background: #c82333; }}
        .htmx-btn {{ background: #28a745; color: white; }}
        .htmx-btn:hover {{ background: #218838; }}
        .result-area {{ margin-top: 20px; min-height: 120px; border: 2px dashed #ccc; padding: 20px; border-radius: 4px; background: #fafafa; }}
        .result-content {{ background: white; padding: 15px; border-radius: 4px; margin-top: 10px; }}
        .regular-result {{ border-left: 3px solid #dc3545; }}
        .htmx-result {{ border-left: 3px solid #28a745; }}
        .comparison-note {{ background: #e3f2fd; padding: 15px; border-radius: 5px; margin: 20px 0; border-left: 4px solid #2196f3; }}
        .stats {{ display: flex; justify-content: space-around; margin: 20px 0; }}
        .stat {{ text-align: center; }}
        .stat-value {{ font-size: 24px; font-weight: bold; }}
        .regular-stat {{ color: #dc3545; }}
        .htmx-stat {{ color: #28a745; }}
        @media (max-width: 768px) {{
            .comparison-container {{ flex-direction: column; }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>HtmxNet Middleware Demo</h1>
            <p>Side-by-Side Comparison: Regular HTTP vs HTMX Requests</p>
        </div>
        
        <div class=""info-box {requestType}"">
            <h3>{requestTitle}</h3>
            <ul>
                <li><strong>Is HTMX Request:</strong> {htmxContext.IsHtmxRequest}</li>
                <li><strong>Target:</strong> {htmxContext.Target ?? "null"}</li>
                <li><strong>Trigger:</strong> {htmxContext.Trigger ?? "null"}</li>
                <li><strong>Current URL:</strong> {htmxContext.CurrentUrl ?? "null"}</li>
                <li><strong>Is Boosted:</strong> {htmxContext.IsBoosted}</li>
                <li><strong>Custom Headers:</strong> {htmxContext.CustomHeaders.Count}</li>
            </ul>
        </div>

        <div class=""comparison-container"">
            <div class=""section regular-section"">
                <h3>🔄 Regular HTTP Requests</h3>
                <p>Traditional approach with full page reloads</p>
                
                <button class=""regular-btn"" onclick=""regularGetRequest()"">Load Content (Full Page)</button>
                <button class=""regular-btn"" onclick=""regularPostRequest()"">Submit Form (Full Page)</button>
                
                <div class=""result-area"" id=""regular-result"">
                    <em>Click a button to see full page reload behavior...</em>
                </div>
            </div>
            
            <div class=""section htmx-section"">
                <h3>⚡ HTMX Requests</h3>
                <p>Modern approach with partial page updates</p>
                
                <button class=""htmx-btn"" hx-get=""/api/simple"" hx-target=""#htmx-result"">Load Content (Partial)</button>
                <button class=""htmx-btn"" hx-post=""/api/form"" hx-target=""#htmx-result"">Submit Form (Partial)</button>
                
                <div class=""result-area"" id=""htmx-result"">
                    <em>Click a button to see partial update behavior...</em>
                </div>
            </div>
        </div>

        <div class=""comparison-note"">
            <h4>💡 Key Differences to Observe:</h4>
            <ul>
                <li><strong>Regular Requests:</strong> Full page reload, slower, more data transfer, page flicker</li>
                <li><strong>HTMX Requests:</strong> Partial updates, faster, less data transfer, smooth experience</li>
                <li><strong>User Experience:</strong> Notice how HTMX feels more like a native app</li>
                <li><strong>Network:</strong> Check browser dev tools to see the difference in request sizes</li>
            </ul>
        </div>
    </div>

    <script>
        function regularGetRequest() {{
            // Simulate regular request behavior
            document.getElementById('regular-result').innerHTML = `
                <div class=""result-content regular-result"">
                    <h4>🔄 Regular GET Request</h4>
                    <p><strong>What happened:</strong></p>
                    <ul>
                        <li>Full page reload initiated</li>
                        <li>Entire HTML document requested</li>
                        <li>Browser navigates to new page</li>
                        <li>All resources reloaded</li>
                    </ul>
                    <p><strong>Result:</strong> Page flicker, slower load, more bandwidth used</p>
                </div>
            `;
        }}

        function regularPostRequest() {{
            // Simulate regular POST behavior
            document.getElementById('regular-result').innerHTML = `
                <div class=""result-content regular-result"">
                    <h4>🔄 Regular POST Request</h4>
                    <p><strong>What happened:</strong></p>
                    <ul>
                        <li>Form submitted via traditional POST</li>
                        <li>Full page reload after submission</li>
                        <li>Browser navigates to response page</li>
                        <li>All page state lost</li>
                    </ul>
                    <p><strong>Result:</strong> Page refresh, slower response, poor UX</p>
                </div>
            `;
        }}
    </script>
</body>
</html>";

    return Results.Content(html, "text/html");
});

// HTMX GET endpoint - shows partial update behavior
app.MapGet("/api/simple", (IHtmxContext htmxContext) =>
{
    var response = $@"<div class=""result-content htmx-result"">
        <h4>⚡ HTMX GET Request</h4>
        <p><strong>What happened:</strong></p>
        <ul>
            <li>Partial content requested via HTMX</li>
            <li>Only this content area updated</li>
            <li>No page reload - smooth experience</li>
            <li>Target Element: {htmxContext.Target ?? "null"}</li>
            <li>Triggered By: {htmxContext.Trigger ?? "Unknown"}</li>
        </ul>
        <p><strong>HTMX Context:</strong></p>
        <ul>
            <li>Is HTMX Request: {htmxContext.IsHtmxRequest}</li>
            <li>Current URL: {htmxContext.CurrentUrl ?? "null"}</li>
            <li>Timestamp: {DateTime.Now:HH:mm:ss}</li>
        </ul>
        <p><strong>Result:</strong> Fast, smooth, native app-like experience</p>
    </div>";

    return Results.Content(response, "text/html");
});

// HTMX POST endpoint - shows partial update behavior
app.MapPost("/api/form", (IHtmxContext htmxContext) =>
{
    var response = $@"<div class=""result-content htmx-result"">
        <h4>⚡ HTMX POST Request</h4>
        <p><strong>What happened:</strong></p>
        <ul>
            <li>Form submitted via HTMX</li>
            <li>Only this content area updated</li>
            <li>No page reload - form state preserved</li>
            <li>Target Element: {htmxContext.Target ?? "null"}</li>
            <li>Triggered By: {htmxContext.Trigger ?? "Unknown"}</li>
        </ul>
        <p><strong>HTMX Context:</strong></p>
        <ul>
            <li>Is HTMX Request: {htmxContext.IsHtmxRequest}</li>
            <li>Current URL: {htmxContext.CurrentUrl ?? "null"}</li>
            <li>Timestamp: {DateTime.Now:HH:mm:ss}</li>
        </ul>
        <p><strong>Result:</strong> Instant feedback, no page refresh, better UX</p>
    </div>";

    return Results.Content(response, "text/html");
});

Console.WriteLine("HtmxNet Demo starting...");
Console.WriteLine("Open your browser to: http://localhost:8080");

app.Run("http://localhost:8080");