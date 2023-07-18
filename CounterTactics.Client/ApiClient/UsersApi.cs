using CounterTactics.Client.Constants;

using Refit;

namespace PolWro.Client.ApiClient;

public partial interface IApiClient
{
    [Post(ApiEndpoints.Test)]
    public Task<IApiResponse> TestAsync();
}