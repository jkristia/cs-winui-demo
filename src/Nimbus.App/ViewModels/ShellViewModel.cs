using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nimbus.App.Services;

namespace Nimbus.App.ViewModels;

/// <summary>Backs the navigation shell: the rail items and the back button state.</summary>
public sealed partial class ShellViewModel : ObservableObject
{
    public INavigationService Navigation { get; }

    public IReadOnlyList<NavigationItem> NavigationItems { get; } =
    [
        new("Dashboard", "", typeof(DashboardViewModel).FullName!),
        new("Projects", "", typeof(ProjectsViewModel).FullName!),
        new("Analytics", "", typeof(AnalyticsViewModel).FullName!),
    ];

    public string SettingsKey { get; } = typeof(SettingsViewModel).FullName!;

    [ObservableProperty]
    private bool _isBackEnabled;

    public ShellViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        Navigation.Navigated += (_, _) => IsBackEnabled = Navigation.CanGoBack;
    }

    [RelayCommand]
    private void GoBack() => Navigation.GoBack();
}
