# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run (development)
dotnet run
# HTTP:  http://localhost:5194
# HTTPS: https://localhost:7252

# Run with specific profile
dotnet run --launch-profile https

# Release build
dotnet build -c Release
```

No test project or linter is configured yet.

## Architecture

**Blazor Server** web application targeting .NET 10. Uses ASP.NET Core with server-side rendering via Razor components.

**Entry point:** `Program.cs` — wires up Razor components, interactive server rendering, and HTTP pipeline.

**Component tree:**
- `Components/App.razor` — root HTML shell, loads Bootstrap and Blazor script
- `Components/Routes.razor` — router; all routes use `MainLayout`
- `Components/Layout/MainLayout.razor` — sidebar + content area layout
- `Components/Layout/NavMenu.razor` — navigation links
- `Components/Pages/` — individual pages (`Home`, `Counter`, `Weather`, `Error`, `NotFound`)

**Rendering modes in use:**
- `@rendermode InteractiveServer` — for stateful/interactive components (e.g. `Counter.razor`)
- `@attribute [StreamRendering]` — for async data loading (e.g. `Weather.razor`)

**Placeholder directories** (empty, ready for feature code):
- `Services/` — intended for business logic / DI services
- `Helpers/` — intended for utility classes

**Styling:** Bootstrap 5 (bundled in `wwwroot/lib/bootstrap/`), with scoped CSS files per component (`.razor.css`).
