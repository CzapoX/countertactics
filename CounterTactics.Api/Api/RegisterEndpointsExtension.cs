namespace CounterTactics.Api.Api;

public static class RegisterEndpointsExtension
{
    public static void RegisterApiEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapUsers();
    }
}