namespace CounterTactics.Api.Persistence.Models;

// This is our TodoUser, we can modify this if we need
// to add custom properties to the user
public class ApplicationUser
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public bool IsActive { get; set; }
}