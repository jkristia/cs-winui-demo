using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nimbus.App.ViewModels.Tabs;

namespace Nimbus.App.ViewModels;

/// <summary>
/// Hosts the vertical analytics tabs. It only owns selection and lazy loading;
/// each tab's data lives in its own <see cref="TabViewModelBase"/>.
/// </summary>
public sealed partial class AnalyticsViewModel : ObservableObject
{
    public ObservableCollection<TabViewModelBase> Tabs { get; }

    [ObservableProperty]
    private TabViewModelBase? _selectedTab;

    public AnalyticsViewModel(
        OverviewTabViewModel overview,
        RevenueTabViewModel revenue,
        AudienceTabViewModel audience,
        PerformanceTabViewModel performance)
    {
        Tabs = [overview, revenue, audience, performance];
    }

    partial void OnSelectedTabChanged(TabViewModelBase? value) =>
        _ = value?.EnsureLoadedAsync();

    [RelayCommand]
    private async Task LoadAsync()
    {
        SelectedTab = Tabs.FirstOrDefault();
        if (SelectedTab is not null)
            await SelectedTab.EnsureLoadedAsync();
    }
}
