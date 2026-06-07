using Nimbus.Core.Models;

namespace Nimbus.Core.Data;

/// <summary>
/// In-memory seed data standing in for a database or REST API.
/// Swapping this out for a real backing store would not touch the UI layer.
/// </summary>
public sealed class MockDataStore
{
    private static readonly DateTimeOffset Now = new(2026, 6, 7, 9, 30, 0, TimeSpan.Zero);

    public IReadOnlyList<TeamMember> TeamMembers { get; } =
    [
        new() { Id = "u1", Name = "Ava Bennett", Role = "Product Lead" },
        new() { Id = "u2", Name = "Marco Diaz", Role = "Senior Engineer" },
        new() { Id = "u3", Name = "Priya Shah", Role = "UX Designer" },
        new() { Id = "u4", Name = "Liam Carter", Role = "Data Analyst" },
        new() { Id = "u5", Name = "Sofia Rossi", Role = "QA Engineer" },
    ];

    public IReadOnlyList<Project> Projects { get; } =
    [
        new()
        {
            Id = "p1", Name = "Apollo Redesign", Client = "Northwind Traders",
            Status = ProjectStatus.Active, Progress = 0.72,
            DueDate = new DateOnly(2026, 7, 15), Budget = 84_000m,
            TeamMemberIds = ["u1", "u2", "u3"],
        },
        new()
        {
            Id = "p2", Name = "Atlas Data Platform", Client = "Contoso Ltd",
            Status = ProjectStatus.Active, Progress = 0.41,
            DueDate = new DateOnly(2026, 9, 1), Budget = 156_000m,
            TeamMemberIds = ["u2", "u4"],
        },
        new()
        {
            Id = "p3", Name = "Helios Mobile App", Client = "Fabrikam",
            Status = ProjectStatus.Planning, Progress = 0.08,
            DueDate = new DateOnly(2026, 10, 20), Budget = 62_000m,
            TeamMemberIds = ["u1", "u3", "u5"],
        },
        new()
        {
            Id = "p4", Name = "Orion Billing Migration", Client = "Tailspin Toys",
            Status = ProjectStatus.OnHold, Progress = 0.55,
            DueDate = new DateOnly(2026, 8, 5), Budget = 98_500m,
            TeamMemberIds = ["u2", "u5"],
        },
        new()
        {
            Id = "p5", Name = "Vega Marketing Site", Client = "Adventure Works",
            Status = ProjectStatus.Completed, Progress = 1.0,
            DueDate = new DateOnly(2026, 5, 30), Budget = 28_000m,
            TeamMemberIds = ["u3", "u4"],
        },
        new()
        {
            Id = "p6", Name = "Nova Analytics Dashboard", Client = "Northwind Traders",
            Status = ProjectStatus.Active, Progress = 0.63,
            DueDate = new DateOnly(2026, 7, 28), Budget = 71_200m,
            TeamMemberIds = ["u1", "u4", "u5"],
        },
    ];

    public IReadOnlyList<ActivityItem> Activity { get; } =
    [
        new() { Id = "a1", ActorName = "Priya Shah", Kind = ActivityKind.FileUpload,
                Summary = "uploaded 12 new hi-fi mockups", ProjectName = "Apollo Redesign",
                Timestamp = Now.AddMinutes(-18) },
        new() { Id = "a2", ActorName = "Marco Diaz", Kind = ActivityKind.StatusChange,
                Summary = "moved 4 tasks to In Review", ProjectName = "Atlas Data Platform",
                Timestamp = Now.AddHours(-2) },
        new() { Id = "a3", ActorName = "Ava Bennett", Kind = ActivityKind.Milestone,
                Summary = "completed the Discovery milestone", ProjectName = "Helios Mobile App",
                Timestamp = Now.AddHours(-5) },
        new() { Id = "a4", ActorName = "Liam Carter", Kind = ActivityKind.Comment,
                Summary = "commented on the Q3 forecast", ProjectName = "Nova Analytics Dashboard",
                Timestamp = Now.AddHours(-8) },
        new() { Id = "a5", ActorName = "Sofia Rossi", Kind = ActivityKind.StatusChange,
                Summary = "flagged a regression in checkout", ProjectName = "Orion Billing Migration",
                Timestamp = Now.AddDays(-1) },
    ];

    public IReadOnlyList<TimeSeriesPoint> MonthlyRevenue { get; } =
    [
        new("Jan", 48_000), new("Feb", 52_500), new("Mar", 61_000),
        new("Apr", 58_200), new("May", 73_400), new("Jun", 81_900),
    ];

    public IReadOnlyList<TrafficSource> TrafficSources { get; } =
    [
        new("Organic Search", 18_420, 0.42),
        new("Direct", 9_870, 0.22),
        new("Referral", 6_540, 0.15),
        new("Social", 5_230, 0.12),
        new("Email", 3_910, 0.09),
    ];

    public IReadOnlyList<ServiceHealth> Services { get; } =
    [
        new() { Name = "API Gateway", UptimePercent = 99.98, LatencyMs = 142 },
        new() { Name = "Auth Service", UptimePercent = 99.95, LatencyMs = 88 },
        new() { Name = "Reporting Worker", UptimePercent = 98.70, LatencyMs = 512 },
        new() { Name = "Media CDN", UptimePercent = 100.0, LatencyMs = 34 },
    ];
}
