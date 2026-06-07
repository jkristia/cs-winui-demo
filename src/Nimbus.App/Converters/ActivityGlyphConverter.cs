using Microsoft.UI.Xaml.Data;
using Nimbus.Core.Models;

namespace Nimbus.App.Converters;

/// <summary>Maps an <see cref="ActivityKind"/> to its Segoe Fluent glyph.</summary>
public sealed class ActivityGlyphConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value switch
        {
            ActivityKind.Comment => "",      // Comment
            ActivityKind.StatusChange => "", // Sync
            ActivityKind.FileUpload => "",   // Upload
            ActivityKind.Milestone => "",    // Flag
            _ => "",                          // Generic info
        };

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotSupportedException();
}
