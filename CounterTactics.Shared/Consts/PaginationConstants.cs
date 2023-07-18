namespace CounterTactics.Shared.Constants;

public static class PaginationConstants
{
    public const int PageSize = 30;
}

public record PaginationResponse<T>(IReadOnlyList<T> Items, int TotalCount);

public record PaginationRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = PaginationConstants.PageSize;
};