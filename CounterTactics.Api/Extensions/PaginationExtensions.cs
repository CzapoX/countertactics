using CounterTactics.Shared.Constants;

using Marten;

namespace CounterTactics.Api.Extensions;

public static class PaginationExtensions
{
    public static async Task<PaginationResponse<T>> Paginate<T>(this IQueryable<T> query, int page,
        int pageSize = PaginationConstants.PageSize, CancellationToken ct = default)
    {
        return new PaginationResponse<T>(await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct),
            await query.CountAsync(ct));
    }
}