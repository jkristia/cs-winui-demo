using System.Collections.ObjectModel;
using System.Globalization;
using Nimbus.Core.Services;

namespace Nimbus.App.ViewModels.Tabs;

/// <summary>Revenue tab: a monthly bar chart plus the underlying figures.</summary>
public sealed partial class RevenueTabViewModel(IAnalyticsService analytics) : TabViewModelBase
{
    private const double ChartHeight = 180;
    private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

    public override string Title => "Revenue";
    public override string Glyph => "";

    public ObservableCollection<RevenueBar> Bars { get; } = [];

    protected override async Task LoadAsync()
    {
        var trend = await analytics.GetRevenueTrendAsync();
        var max = trend.Max(p => p.Value);

        Bars.Clear();
        foreach (var point in trend)
        {
            // Scale to pixels here so the view only ever sets a Height.
            var height = max > 0 ? point.Value / max * ChartHeight : 0;
            Bars.Add(new RevenueBar(
                point.Label,
                ((decimal)point.Value).ToString("C0", Usd),
                Math.Round(height)));
        }
    }
}
