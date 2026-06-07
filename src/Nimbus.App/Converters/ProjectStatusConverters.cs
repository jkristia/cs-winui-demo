using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Nimbus.Core.Models;

namespace Nimbus.App.Converters;

/// <summary>Turns a <see cref="ProjectStatus"/> into its human-friendly label.</summary>
public sealed class ProjectStatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value switch
        {
            ProjectStatus.Planning => "Planning",
            ProjectStatus.Active => "Active",
            ProjectStatus.OnHold => "On Hold",
            ProjectStatus.Completed => "Completed",
            _ => string.Empty,
        };

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}

/// <summary>
/// Picks a status pill brush. ConverterParameter "Background" returns the soft
/// fill; anything else returns the strong foreground colour.
/// </summary>
public sealed class ProjectStatusBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var background = string.Equals(parameter as string, "Background", StringComparison.OrdinalIgnoreCase);

        var key = value switch
        {
            ProjectStatus.Planning => "SystemFillColorCaution",
            ProjectStatus.Active => "SystemFillColorSuccess",
            ProjectStatus.OnHold => "SystemFillColorCritical",
            ProjectStatus.Completed => "SystemFillColorNeutral",
            _ => "SystemFillColorNeutral",
        };

        key += background ? "BackgroundBrush" : "Brush";
        return BrushResources.Lookup(key);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
