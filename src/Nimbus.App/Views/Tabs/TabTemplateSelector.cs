using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Nimbus.App.ViewModels.Tabs;

namespace Nimbus.App.Views.Tabs;

/// <summary>Chooses the right tab view for a given analytics tab view-model.</summary>
public sealed class TabTemplateSelector : DataTemplateSelector
{
    public DataTemplate? Overview { get; set; }
    public DataTemplate? Revenue { get; set; }
    public DataTemplate? Audience { get; set; }
    public DataTemplate? Performance { get; set; }

    protected override DataTemplate SelectTemplateCore(object item) =>
        item switch
        {
            OverviewTabViewModel => Overview!,
            RevenueTabViewModel => Revenue!,
            AudienceTabViewModel => Audience!,
            PerformanceTabViewModel => Performance!,
            _ => base.SelectTemplateCore(item),
        };

    protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) =>
        SelectTemplateCore(item);
}
