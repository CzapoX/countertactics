using Blazored.LocalStorage;

using CounterTactics.Client;
using CounterTactics.Client.Extensions;
using CounterTactics.Client.Filters;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor;
using MudBlazor.Services;

using PolWro.Client.ApiClient;

using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMsalAuthentication(options =>
{
    options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration["AzureAdB2C:DefaultScope"]!);
    options.ProviderOptions.LoginMode = "redirect";
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
});

builder.Services.AddTransient<CustomHttpClientDelegatingHandler>();

builder.Services
    .AddRefitClient<IApiClient>()
    .ConfigureHttpClient(client => { client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUrl")!); })
    .AddHttpMessageHandler<CustomHttpClientDelegatingHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});

builder.Services.AddOptions();
builder.Services.AddLocalization(c => c.ResourcesPath = "Resources");

var host = builder.Build();

await host.ConfigureLocalization();

await host.RunAsync();