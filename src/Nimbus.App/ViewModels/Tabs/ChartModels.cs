namespace Nimbus.App.ViewModels.Tabs;

/// <summary>A single bar in the revenue chart, pre-scaled to pixels by the view-model.</summary>
public sealed record RevenueBar(string Label, string DisplayValue, double BarHeight);

/// <summary>A simple headline tile used on the analytics overview tab.</summary>
public sealed record StatTile(string Label, string Value, string Caption);

/// <summary>A traffic channel with its share expressed as a 0..100 percentage.</summary>
public sealed record TrafficShare(string Name, string Sessions, double Percent, string PercentText);

/// <summary>A monitored service row with display-ready health figures.</summary>
public sealed record ServiceRow(
    string Name,
    string StatusText,
    string UptimeText,
    string LatencyText,
    double UptimePercent,
    bool IsHealthy);
