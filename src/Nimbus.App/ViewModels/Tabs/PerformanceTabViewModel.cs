using System.Collections.ObjectModel;
using System.Globalization;
using Nimbus.Core.Services;

namespace Nimbus.App.ViewModels.Tabs;

/// <summary>Performance tab: live-ish service health and latency.</summary>
public sealed partial class PerformanceTabViewModel(IAnalyticsService analytics) : TabViewModelBase
{
    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public override string Title => "Performance";
    public override string Glyph => "";

    public ObservableCollection<ServiceRow> Services { get; } = [];

    protected override async Task LoadAsync()
    {
        var services = await analytics.GetServiceHealthAsync();

        Services.Clear();
        foreach (var s in services)
        {
            Services.Add(new ServiceRow(
                s.Name,
                s.IsHealthy ? "Healthy" : "Degraded",
                $"{s.UptimePercent.ToString("0.00", Inv)}% uptime",
                $"{s.LatencyMs} ms",
                s.UptimePercent,
                s.IsHealthy));
        }
    }
}
