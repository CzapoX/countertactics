using System.Security.Claims;

using CounterTactics.Api.Persistence.Models;

using Marten;

using Microsoft.AspNetCore.Authentication;

using Serilog;

namespace CounterTactics.Api.Authorization;

public static class CurrentUserExtensions
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<CurrentUser>();
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
        return services;
    }

    private sealed class ClaimsTransformation : IClaimsTransformation
    {
        private readonly CurrentUser _currentUser;
        private readonly IDocumentSession _documentSession;

        public ClaimsTransformation(CurrentUser currentUser, IDocumentSession documentSession)
        {
            _currentUser = currentUser;
            _documentSession = documentSession;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // We're not going to transform anything. We're using this as a hook into authorization
            // to set the current user without adding custom middleware.

            if (principal.FindFirstValue(ClaimTypes.NameIdentifier) is not { Length: > 0 } id)
                return principal;

            // Resolve the user manager and see if the current user is a valid user in the database
            // we do this once and store it on the current user.
            var user = await _documentSession.LoadAsync<ApplicationUser>(id);
            if (user is null)
            {
                user = new ApplicationUser
                {
                    Id = id,
                    UserName = principal.FindFirstValue("name")!,
                    IsActive = true
                };
                _documentSession.Store(user);
                await _documentSession.SaveChangesAsync();
                Log.Information("Created new user {@User}", user);
            }

            _currentUser.User = user;

            return principal;
        }
    }
}