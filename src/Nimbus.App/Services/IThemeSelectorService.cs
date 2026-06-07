using Microsoft.UI.Xaml;

namespace Nimbus.App.Services;

/// <summary>Applies the requested element theme to the app's root content.</summary>
public interface IThemeSelectorService
{
    ElementTheme Theme { get; }
    void SetTheme(ElementTheme theme);
}
