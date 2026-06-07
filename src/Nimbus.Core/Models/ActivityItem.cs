namespace Nimbus.Core.Models;

public enum ActivityKind
{
    Comment,
    StatusChange,
    FileUpload,
    Milestone
}

/// <summary>A single entry in the workspace activity feed.</summary>
public sealed record ActivityItem
{
    public string Id { get; init; } = "";
    public string ActorName { get; init; } = "";
    public string Summary { get; init; } = "";
    public string ProjectName { get; init; } = "";
    public ActivityKind Kind { get; init; }
    public DateTimeOffset Timestamp { get; init; }
}
