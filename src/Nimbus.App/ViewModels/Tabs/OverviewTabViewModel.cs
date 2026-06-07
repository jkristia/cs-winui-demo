using System.Collections.ObjectModel;
using System.Globalization;
using Nimbus.Core.Services;

namespace Nimbus.App.ViewModels.Tabs;

/// <summary>First analytics tab: a high-level snapshot across all data sources.</summary>
public sealed partial class OverviewTabViewModel(IAnalyticsService analytics) : TabViewModelBase
{
    private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

    public override string Title => "Overview";
    public override string Glyph => "";

    public ObservableCollection<StatTile> Tiles { get; } = [];

    protected override async Task LoadAsync()
    {
        var revenue = await analytics.GetRevenueTrendAsync();
        var projected = await analytics.GetProjectedAnnualRevenueAsync();
        var traffic = await analytics.GetTrafficSourcesAsync();
        var services = await analytics.GetServiceHealthAsync();

        var totalSessions = traffic.Sum(t => t.Sessions);
        var healthy = services.Count(s => s.IsHealthy);

        Tiles.Clear();
        Tiles.Add(new StatTile("Projected ARR", projected.ToString("C0", Usd),
            "Annualised run-rate"));
        Tiles.Add(new StatTile("This Month", revenue[^1].Value.ToString("C0", Usd),
            $"{revenue[^1].Label} revenue"));
        Tiles.Add(new StatTile("Total Sessions", totalSessions.ToString("N0", Usd),
            "Across all channels"));
        Tiles.Add(new StatTile("Services Healthy", $"{healthy}/{services.Count}",
            "Within SLA targets"));
    }
}
