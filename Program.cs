using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TaskManager.Blazor.Components;
using TaskManager.Blazor.Services;

var builder =
    WebApplication.CreateBuilder(args);


// ========================
// Razor Components
// ========================

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();


// ========================
// Configuration
// ========================

string apiBaseUrl =
    builder.Configuration[
        "ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException(
        "ApiSettings:BaseUrl is missing.");


// ========================
// Authentication State
// ========================

builder.Services.AddScoped<
    ProtectedSessionStorage>();


builder.Services.AddScoped<
    AuthStateService>();


builder.Services.AddAuthorizationCore();


builder.Services.AddScoped<
    CustomAuthenticationStateProvider>();


builder.Services.AddScoped<
    AuthenticationStateProvider>(
        serviceProvider =>
            serviceProvider
                .GetRequiredService<
                    CustomAuthenticationStateProvider>());


// ========================
// Guards
// ========================

builder.Services.AddScoped<
    AuthenticatedPageGuard>();


// ========================
// HTTP
// ========================

builder.Services.AddHttpClient(
    "TaskManagerApi",
    client =>
    {
        client.BaseAddress =
            new Uri(
                apiBaseUrl);
    });


builder.Services.AddScoped<
    ApiRequestFactory>();


// ========================
// API Clients
// ========================

builder.Services.AddScoped<
    AuthApiClient>();


builder.Services.AddScoped<
    TaskApiClient>();


// ========================
// Helpers
// ========================

builder.Services.AddScoped<
    ApiErrorMessageProvider>();


var app =
    builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors:
            true);

    app.UseHsts();
}


app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages:
        true);


app.UseHttpsRedirection();


app.UseAntiforgery();


app.MapStaticAssets();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();