using CounterTactics.Api.Authorization;
using CounterTactics.Api.Extensions;
using CounterTactics.Api.Persistence.Models;

using Marten;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Graph;

namespace CounterTactics.Api.Api;

public static class UsersApi
{
    public static void MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users");

        group.RequireAuthorization(pb => pb.RequireCurrentUser())
            .AddOpenApiSecurityRequirement();

        group.MapPost("/", TestAsync);
    }


    private static async Task<Results<NoContent, BadRequest>> TestAsync(GraphServiceClient graphClient,
        IDocumentSession documentSession, CancellationToken ct)
    {
        var graphUser = await graphClient.Users.GetAsync(cancellationToken:ct);
        if (graphUser?.Value is null)
            return TypedResults.BadRequest();

        var users = graphUser.Value.Select(x => new ApplicationUser
        {
            UserName = x.DisplayName!,
            Id = x.Id!
        });

        documentSession.StoreObjects(users);
        await documentSession.SaveChangesAsync(ct);
        return TypedResults.NoContent();
    }
}