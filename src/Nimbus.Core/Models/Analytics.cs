namespace Nimbus.Core.Models;

/// <summary>One point in a time series (e.g. monthly revenue).</summary>
public sealed record TimeSeriesPoint(string Label, double Value);

/// <summary>A named channel contributing a share of traffic.</summary>
public sealed record TrafficSource(string Name, double Sessions, double Share);

/// <summary>A monitored service and its current health figures.</summary>
public sealed record ServiceHealth
{
    public string Name { get; init; } = "";
    public double UptimePercent { get; init; }
    public int LatencyMs { get; init; }

    /// <summary>True when the service is operating within its targets.</summary>
    public bool IsHealthy => UptimePercent >= 99.0 && LatencyMs < 400;
}
