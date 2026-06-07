using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace Nimbus.App.Converters;

/// <summary>Formats a <see cref="DateTimeOffset"/> as a compact "MMM d · h:mm tt" label.</summary>
public sealed class ShortTimestampConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTimeOffset timestamp)
            return timestamp.ToString("MMM d · h:mm tt", CultureInfo.InvariantCulture);

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
