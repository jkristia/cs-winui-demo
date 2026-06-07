using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace Nimbus.App.Converters;

/// <summary>Formats a 0..1 ratio as a whole-number percentage, e.g. 0.72 -> "72%".</summary>
public sealed class PercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var ratio = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
        return ratio.ToString("P0", CultureInfo.InvariantCulture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
