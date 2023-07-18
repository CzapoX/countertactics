using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CounterTactics.Client.Filters;

public class CustomHttpClientDelegatingHandler : AuthorizationMessageHandler
{
    public CustomHttpClientDelegatingHandler(IAccessTokenProvider provider, NavigationManager navigation,
        IConfiguration configuration) : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { configuration["ApiUrl"] });
    }
}