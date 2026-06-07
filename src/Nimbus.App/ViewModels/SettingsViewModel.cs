using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Nimbus.App.Services;

namespace Nimbus.App.ViewModels;

/// <summary>Settings page: theme selection plus a few illustrative preferences.</summary>
public sealed partial class SettingsViewModel : ObservableObject
{
    private readonly IThemeSelectorService _themeSelector;

    public string AppVersion { get; } = "Nimbus 1.0.0 (demo build)";

    /// <summary>Index into <see cref="ElementTheme"/>: Default / Light / Dark.</summary>
    [ObservableProperty]
    private int _selectedThemeIndex;

    [ObservableProperty]
    private bool _emailNotifications = true;

    [ObservableProperty]
    private bool _weeklyDigest;

    [ObservableProperty]
    private bool _useCompactSpacing;

    public SettingsViewModel(IThemeSelectorService themeSelector)
    {
        _themeSelector = themeSelector;
        _selectedThemeIndex = (int)themeSelector.Theme;
    }

    partial void OnSelectedThemeIndexChanged(int value) =>
        _themeSelector.SetTheme((ElementTheme)value);
}
