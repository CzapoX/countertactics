using CounterTactics.Api.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace CounterTactics.Api.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var azureAdSection = configuration.GetSection("AzureAdB2C");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(azureAdSection)
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches()
            .AddMicrosoftGraph();
        
        services.AddAuthorizationBuilder().AddCurrentUserHandler();
        services.AddCurrentUser();
        return services;
    }
}