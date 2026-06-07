using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nimbus.Core.Models;
using Nimbus.Core.Services;

namespace Nimbus.App.ViewModels;

/// <summary>Portfolio view: a filterable list of projects with their teams.</summary>
public sealed partial class ProjectsViewModel : ObservableObject
{
    private readonly IProjectService _projects;

    public ObservableCollection<ProjectCard> Projects { get; } = [];

    /// <summary>Filter choices shown in the combo box; null = "All".</summary>
    public IReadOnlyList<string> StatusFilters { get; } =
        ["All", "Planning", "Active", "On Hold", "Completed"];

    [ObservableProperty]
    private string _selectedStatusFilter = "All";

    [ObservableProperty]
    private bool _isLoading;

    public ProjectsViewModel(IProjectService projects) => _projects = projects;

    partial void OnSelectedStatusFilterChanged(string value) => _ = LoadAsync();

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsLoading = true;

        var status = SelectedStatusFilter switch
        {
            "Planning" => ProjectStatus.Planning,
            "Active" => ProjectStatus.Active,
            "On Hold" => ProjectStatus.OnHold,
            "Completed" => ProjectStatus.Completed,
            _ => (ProjectStatus?)null,
        };

        var projects = await _projects.GetProjectsAsync(status);

        Projects.Clear();
        foreach (var project in projects)
        {
            var team = await _projects.GetTeamAsync(project);
            Projects.Add(new ProjectCard(project, team));
        }

        IsLoading = false;
    }
}
