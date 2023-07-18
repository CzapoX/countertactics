namespace CounterTactics.Api.Persistence.Models;

public abstract class BaseTrackingEntity
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset ModifiedAt { get; set; } = DateTimeOffset.UtcNow;
}