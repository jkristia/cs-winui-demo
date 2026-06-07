using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Nimbus.Core.Models;

namespace Nimbus.App.Controls;

/// <summary>
/// A presentation-only KPI card. It carries no logic beyond exposing the
/// <see cref="Metric"/> it should render via a dependency property.
/// </summary>
public sealed partial class MetricCard : UserControl
{
    public static readonly DependencyProperty MetricProperty =
        DependencyProperty.Register(
            nameof(Metric),
            typeof(Metric),
            typeof(MetricCard),
            new PropertyMetadata(null));

    public Metric? Metric
    {
        get => (Metric?)GetValue(MetricProperty);
        set => SetValue(MetricProperty, value);
    }

    public MetricCard() => InitializeComponent();
}
