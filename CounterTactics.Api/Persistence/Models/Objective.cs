using CounterTactics.Api.Persistence.Enums;

namespace CounterTactics.Api.Persistence.Models;

public class Objective : BaseTrackingEntity
{
    public ObjectiveType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int[] VictoryPointRewards { get; set; } = Array.Empty<int>();
}