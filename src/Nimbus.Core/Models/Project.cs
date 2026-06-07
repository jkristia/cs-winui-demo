namespace Nimbus.Core.Models;

/// <summary>A unit of work tracked in the workspace.</summary>
public sealed record Project
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string Client { get; init; } = "";
    public ProjectStatus Status { get; init; }

    /// <summary>Completion ratio in the range 0..1.</summary>
    public double Progress { get; init; }

    public DateOnly DueDate { get; init; }
    public decimal Budget { get; init; }
    public IReadOnlyList<string> TeamMemberIds { get; init; } = [];
}
