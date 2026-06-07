using System.Collections.ObjectModel;
using System.Globalization;
using Nimbus.Core.Services;

namespace Nimbus.App.ViewModels.Tabs;

/// <summary>Audience tab: where traffic comes from, as share bars.</summary>
public sealed partial class AudienceTabViewModel(IAnalyticsService analytics) : TabViewModelBase
{
    private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public override string Title => "Audience";
    public override string Glyph => "";

    public ObservableCollection<TrafficShare> Sources { get; } = [];

    protected override async Task LoadAsync()
    {
        var sources = await analytics.GetTrafficSourcesAsync();

        Sources.Clear();
        foreach (var source in sources)
        {
            var percent = Math.Round(source.Share * 100);
            Sources.Add(new TrafficShare(
                source.Name,
                source.Sessions.ToString("N0", Inv),
                percent,
                $"{percent:0}%"));
        }
    }
}
