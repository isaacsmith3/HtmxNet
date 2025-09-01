# HtmxNet

A .NET library that provides integration between htmx and ASP.NET Core applications.

## Project Status

**COMPLETED:**

- **HtmxMiddleware**: Fully implemented and working
- **Core Context**: All htmx headers parsed and accessible
- **Dependency Injection**: Complete ASP.NET Core integration
- **Demo Application**: Working browser demo available

**IN PROGRESS:**

- HTML Helpers for generating htmx attributes
- Controller Extensions for simplified responses
- Response Builder for complex htmx responses

## Features

- **HtmxMiddleware**: Automatically parses htmx headers and provides strongly-typed access
- **Dependency Injection**: Easy registration with ASP.NET Core DI container
- **Thread-safe**: Safe for concurrent requests
- **Zero-configuration**: Works out of the box with sensible defaults
- **Progressive Enhancement**: Seamlessly handles both regular and htmx requests

## 📦 Project Structure

```
htmx-net/
├── src/HtmxNet/              # Main library
│   ├── Abstractions/         # Interfaces
│   ├── Core/                 # Core implementations
│   └── Middleware/           # ASP.NET Core middleware
├── samples/Demo/             # Working demo application
└── .kiro/specs/              # Development specifications
```

## 🚀 Quick Start

### 1. Clone and Build

```bash
git clone <repository-url>
cd htmx-net
dotnet build src/HtmxNet/HtmxNet.csproj
```

### 2. Run the Demo

```bash
dotnet run --project samples/Demo/Demo.csproj
# Open browser to http://localhost:8080
```

### 3. Use in Your Project

```csharp
// Program.cs
using HtmxNet.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add htmx services
builder.Services.AddHtmx();

var app = builder.Build();

// Use htmx middleware
app.UseHtmx();

// Use in endpoints
app.MapGet("/", (IHtmxContext htmxContext) =>
{
    if (htmxContext.IsHtmxRequest)
    {
        // Return partial content for htmx requests
        return Results.Content("<div>Partial content</div>", "text/html");
    }

    // Return full page for regular requests
    return Results.Content("<html><body>Full page</body></html>", "text/html");
});

// In MVC Controllers
public class HomeController : Controller
{
    public IActionResult Index([FromServices] IHtmxContext htmxContext)
    {
        if (htmxContext.IsHtmxRequest)
        {
            return PartialView("_IndexPartial");
        }

        return View();
    }
}
```

## 🔍 HtmxContext Properties

The `IHtmxContext` provides access to all htmx request headers:

| Property                  | Description                                  | Example                      |
| ------------------------- | -------------------------------------------- | ---------------------------- |
| `IsHtmxRequest`           | True if request was made by htmx             | `true`                       |
| `Target`                  | ID of the target element                     | `"#content"`                 |
| `Trigger`                 | ID of the element that triggered the request | `"submit-btn"`               |
| `TriggerName`             | Name attribute of the triggering element     | `"submit"`                   |
| `CurrentUrl`              | Current URL of the page                      | `"https://example.com/page"` |
| `PromptResponse`          | Response to hx-prompt                        | `"user input"`               |
| `IsBoosted`               | True if request is via hx-boost              | `true`                       |
| `IsHistoryRestoreRequest` | True if request is for history restoration   | `false`                      |
| `CustomHeaders`           | Dictionary of custom htmx headers            | `{"HX-Custom": "value"}`     |

## ⚙️ Configuration

```csharp
builder.Services.AddHtmx(options =>
{
    options.AutoDetectPartialViews = true;
    options.PartialViewSuffix = "_Partial";
    options.EnableDetailedErrors = false;
    options.DefaultHeaders.Add("X-Custom", "value");
});
```

## The Demo

The demo application showcases all middleware features:

1. **Initial Load**: Shows regular HTTP request detection
2. **HTMX Buttons**: Demonstrates header parsing and context population
3. **Real-time Updates**: See how the middleware processes different request types
4. **Interactive Examples**: Click buttons to see the middleware in action

**What you'll see:**

- Request type detection (Regular vs HTMX)
- Header parsing in real-time
- Target element identification
- Trigger element tracking
- Custom header handling

## Development

This project follows a spec-driven development approach. See `.kiro/specs/htmx-dotnet-integration/` for:

- Requirements documentation
- Technical design
- Implementation tasks and progress

## Requirements

- .NET 9.0 or later
- ASP.NET Core 9.0 or later

## License

This project is licensed under the MIT License.
