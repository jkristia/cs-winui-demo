using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nimbus.Core.Models;
using Nimbus.Core.Services;
using Nimbus.App.Services;

namespace Nimbus.App.ViewModels;

/// <summary>Landing page: KPI cards, the most active projects, and the activity feed.</summary>
public sealed partial class DashboardViewModel : ObservableObject
{
    private readonly IDashboardService _dashboard;
    private readonly INavigationService _navigation;

    public ObservableCollection<Metric> Kpis { get; } = [];
    public ObservableCollection<Project> ActiveProjects { get; } = [];
    public ObservableCollection<ActivityItem> RecentActivity { get; } = [];

    public string Greeting => "Good morning, Ava";
    public string Subtitle => "Here's what's happening across your workspace today.";

    [ObservableProperty]
    private bool _isLoading;

    public DashboardViewModel(IDashboardService dashboard, INavigationService navigation)
    {
        _dashboard = dashboard;
        _navigation = navigation;
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsLoading = true;

        Kpis.Clear();
        foreach (var kpi in await _dashboard.GetKpisAsync())
            Kpis.Add(kpi);

        ActiveProjects.Clear();
        foreach (var project in await _dashboard.GetActiveProjectsAsync())
            ActiveProjects.Add(project);

        RecentActivity.Clear();
        foreach (var activity in await _dashboard.GetRecentActivityAsync())
            RecentActivity.Add(activity);

        IsLoading = false;
    }

    [RelayCommand]
    private void ViewAllProjects() =>
        _navigation.NavigateTo(typeof(ProjectsViewModel).FullName!);
}
