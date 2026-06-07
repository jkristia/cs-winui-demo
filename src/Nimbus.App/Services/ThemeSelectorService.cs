using Microsoft.UI.Xaml;

namespace Nimbus.App.Services;

/// <summary>Sets <see cref="FrameworkElement.RequestedTheme"/> on the window's root.</summary>
public sealed class ThemeSelectorService : IThemeSelectorService
{
    public ElementTheme Theme { get; private set; } = ElementTheme.Default;

    public void SetTheme(ElementTheme theme)
    {
        Theme = theme;

        if (App.MainWindow?.Content is FrameworkElement root)
            root.RequestedTheme = theme;
    }
}
