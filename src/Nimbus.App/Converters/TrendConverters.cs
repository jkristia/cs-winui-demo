using System.Globalization;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Nimbus.App.Converters;

internal static class BrushResources
{
    public static Brush Lookup(string key) =>
        (Brush)Microsoft.UI.Xaml.Application.Current.Resources[key];
}

/// <summary>true -> success (green) brush, false -> critical (red) brush.</summary>
public sealed class TrendBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        BrushResources.Lookup(value is true
            ? "SystemFillColorSuccessBrush"
            : "SystemFillColorCriticalBrush");

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}

/// <summary>true -> up-arrow glyph, false -> down-arrow glyph.</summary>
public sealed class TrendGlyphConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value is true ? "" : "";

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}

/// <summary>Formats a percentage with an explicit sign, e.g. 12.5 -> "+12.5%".</summary>
public sealed class SignedPercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var number = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
        var sign = number >= 0 ? "+" : string.Empty;
        return $"{sign}{number.ToString("0.#", CultureInfo.InvariantCulture)}%";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
