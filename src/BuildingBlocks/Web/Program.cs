using Infrastructure;
using Infrastructure.Context;
using Scalar.AspNetCore;
using SharedKernel.Constants;
using System.Globalization;
using Web.Extensions;
using Web.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder
    .AddSqlServerDbContext<ApplicationDbContext>
    (AppSetting.ConnectionString);

builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
var culture = new CultureInfo("fa-IR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var app = builder.Build();
await app.ApplyModuleMigrationsAsync();
// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Abolfazl and hamed");
    options.RoutePrefix = string.Empty;
});
app.MapOpenApi("/api/openapi/{documentName}.json");
app.MapScalarApiReference("/api/docs", options =>
{
    options
        .WithTitle("Scalar Demo API")
        .WithTheme(ScalarTheme.None)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithOpenApiRoutePattern("/api/openapi/{documentName}.json")
        .DisableDefaultFonts()
        .ForceDarkMode()
        .WithCustomCss("""
            .dark-mode,
            :root {
                --scalar-color-1: #F2F2F8;
                --scalar-color-2: #A8A8C0;
                --scalar-color-3: #6E6E90;
                --scalar-color-accent: #60A5FA;
                --scalar-background-1: #080810;
                --scalar-background-2: #0E0E1A;
                --scalar-background-3: #131320;
                --scalar-background-accent: rgba(37, 99, 235, 0.08);
                --scalar-border-color: #282846;
                --scalar-button-1: #2563EB;
                --scalar-button-1-hover: #1D4ED8;
                --scalar-button-1-color: #F2F2F8;
                --scalar-shadow-1: 0 1px 3px 0 rgba(0, 0, 0, 0.3);
                --scalar-shadow-2: 0 0 0 0.5px #282846, 0 12px 24px rgba(0, 0, 0, 0.4);
                --scalar-scrollbar-color: rgba(168, 168, 192, 0.15);
                --scalar-scrollbar-color-active: rgba(168, 168, 192, 0.3);
            }

            .dark-mode .sidebar,
            .sidebar {
                --scalar-sidebar-background-1: #0E0E1A;
                --scalar-sidebar-border-color: #282846;
                --scalar-sidebar-color-1: #F2F2F8;
                --scalar-sidebar-color-2: #A8A8C0;
                --scalar-sidebar-color-active: #60A5FA;
                --scalar-sidebar-item-hover-background: #1A1A2E;
                --scalar-sidebar-item-active-background: rgba(37, 99, 235, 0.12);
                --scalar-sidebar-search-background: #131320;
                --scalar-sidebar-search-border-color: #282846;
                --scalar-sidebar-search-color: #6E6E90;
            }

            :root {
                --scalar-font: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
                --scalar-font-code: 'JetBrains Mono', 'SF Mono', 'Cascadia Code', 'Fira Code', ui-monospace, monospace;
            }

            .dark-mode pre,
            pre {
                background: #0E0E1A !important;
                border: 1px solid #282846;
                border-radius: 8px;
            }

            .introduction-description .markdown code:not(pre code),
            .markdown code:not(pre code) {
                background: rgba(37, 99, 235, 0.1);
                color: #93C5FD;
                padding: 0.15em 0.4em;
                border-radius: 4px;
                font-size: 0.875em;
                border: 1px solid rgba(37, 99, 235, 0.15);
            }

            .introduction-description .markdown a {
                color: #60A5FA;
                text-decoration: none;
                border-bottom: 1px solid rgba(96, 165, 250, 0.3);
                transition: all 0.15s ease;
            }

            .introduction-description .markdown a:hover {
                color: #93C5FD;
                border-bottom-color: #60A5FA;
            }

            .introduction-description .markdown table,
            .introduction-description .markdown td {
                border-color: #282846;
            }

            .introduction-description .markdown th {
                background: #131320;
                color: #F2F2F8;
                border-color: #282846;
            }

            .scalar-api-reference [data-method="post"],
            .endpoint .post {
                color: #60A5FA;
            }

            .scalar-api-reference [data-method="get"],
            .endpoint .get {
                color: #A5D6A7;
            }

            .scalar-api-reference [data-method="put"],
            .endpoint .put {
                color: #FFCB6B;
            }

            .scalar-api-reference [data-method="delete"],
            .endpoint .delete {
                color: #FF9CAC;
            }

            .scalar-api-reference button:focus-visible,
            .scalar-api-reference a:focus-visible {
                outline: 2px solid rgba(96, 165, 250, 0.5);
                outline-offset: 2px;
            }
            """);

    options.Servers = [];
});
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapMyEndpoints();

app.Run();

