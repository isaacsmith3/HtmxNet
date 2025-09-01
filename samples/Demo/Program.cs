using HtmxNet.Middleware;
using HtmxNet.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add htmx services
builder.Services.AddHtmx();

var app = builder.Build();

// Use htmx middleware
app.UseHtmx();

// Home page - shows both regular and htmx request info
app.MapGet("/", (IHtmxContext htmxContext) =>
{
    var requestType = htmxContext.IsHtmxRequest ? "htmx-request" : "regular-request";
    var requestTitle = htmxContext.IsHtmxRequest ? "HTMX Request Detected!" : "Regular HTTP Request";
    
    var html = $@"<!DOCTYPE html>
<html>
<head>
    <title>HtmxNet Middleware Demo</title>
    <script src=""https://unpkg.com/htmx.org@1.9.10""></script>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; }}
        .container {{ max-width: 800px; margin: 0 auto; }}
        .info-box {{ background: #f5f5f5; padding: 20px; margin: 20px 0; border-radius: 5px; }}
        .htmx-request {{ background: #e8f5e8; }}
        .regular-request {{ background: #fff3cd; }}
        button {{ padding: 10px 20px; margin: 10px 5px; cursor: pointer; }}
        .demo-section {{ margin: 30px 0; padding: 20px; border: 1px solid #ddd; border-radius: 5px; }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>HtmxNet Middleware Demo</h1>
        
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

        <div class=""demo-section"">
            <h3>Try HTMX Requests</h3>
            <p>Click these buttons to see the middleware in action:</p>
            
            <button hx-get=""/api/simple"" hx-target=""#result"">Simple GET</button>
            <button hx-post=""/api/form"" hx-target=""#result"">POST Request</button>
            
            <div id=""result"" style=""margin-top: 20px; min-height: 100px; border: 1px dashed #ccc; padding: 20px;"">
                <em>Results will appear here...</em>
            </div>
        </div>
    </div>
</body>
</html>";

    return Results.Content(html, "text/html");
});

// Simple API endpoint
app.MapGet("/api/simple", (IHtmxContext htmxContext) =>
{
    var response = $@"<div style=""background: #d4edda; padding: 15px; border-radius: 5px;"">
        <h4>Simple GET Request Processed</h4>
        <p><strong>Request Details:</strong></p>
        <ul>
            <li>Target Element: {htmxContext.Target ?? "null"}</li>
            <li>Triggered By: {htmxContext.Trigger ?? "Unknown"}</li>
            <li>Is HTMX: {htmxContext.IsHtmxRequest}</li>
            <li>Timestamp: {DateTime.Now:HH:mm:ss}</li>
        </ul>
    </div>";
    
    return Results.Content(response, "text/html");
});

// POST endpoint
app.MapPost("/api/form", (IHtmxContext htmxContext) =>
{
    var response = $@"<div style=""background: #cce5ff; padding: 15px; border-radius: 5px;"">
        <h4>POST Request Received</h4>
        <p><strong>HTMX Context:</strong></p>
        <ul>
            <li>Is HTMX Request: {htmxContext.IsHtmxRequest}</li>
            <li>Target: {htmxContext.Target ?? "null"}</li>
            <li>Trigger: {htmxContext.Trigger ?? "null"}</li>
            <li>Current URL: {htmxContext.CurrentUrl ?? "null"}</li>
        </ul>
    </div>";
    
    return Results.Content(response, "text/html");
});

Console.WriteLine("HtmxNet Demo starting...");
Console.WriteLine("Open your browser to: http://localhost:8080");

app.Run("http://localhost:8080");