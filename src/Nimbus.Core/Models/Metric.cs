namespace Nimbus.Core.Models;

/// <summary>A headline number shown as a KPI card on the dashboard.</summary>
public sealed record Metric
{
    public string Label { get; init; } = "";
    public string Value { get; init; } = "";

    /// <summary>Percentage change versus the previous period (may be negative).</summary>
    public double ChangePercent { get; init; }

    /// <summary>Glyph from the Segoe Fluent Icons font.</summary>
    public string Glyph { get; init; } = "";

    public bool IsPositive => ChangePercent >= 0;
}
