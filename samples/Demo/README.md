# HtmxNet Demo Application

This demo showcases the HtmxMiddleware functionality with a live web application.

## Running the Demo

1. **Navigate to the demo directory:**

   ```bash
   cd samples/Demo
   ```

2. **Run the application:**

   ```bash
   dotnet run
   ```

3. **Open your browser to:**
   ```
   http://localhost:5000
   ```

## What the Demo Shows

- **Request Detection**: See how the middleware automatically detects HTMX vs regular HTTP requests
- **Header Parsing**: View all parsed HTMX headers (Target, Trigger, Current URL, etc.)
- **Interactive Examples**: Click buttons to see HTMX requests in action
- **Real-time Context**: Watch how the `IHtmxContext` populates with each request

## Features Demonstrated

1. **Simple GET Requests**: Basic HTMX button clicks
2. **POST Requests**: Form submissions via HTMX
3. **Context Information**: Real-time display of all HTMX context properties
4. **Middleware Integration**: Shows seamless ASP.NET Core integration

## Key Files

- `Program.cs`: Main application with middleware setup and endpoints
- `Demo.csproj`: Project configuration with HtmxNet reference

The demo uses the HtmxNet library from `../../src/HtmxNet/` to demonstrate real-world usage patterns.
