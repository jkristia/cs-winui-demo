using Microsoft.UI.Xaml.Controls;
using Nimbus.App.ViewModels;

namespace Nimbus.App.Views;

public sealed partial class ProjectsPage : Page
{
    public ProjectsViewModel ViewModel { get; }

    public ProjectsPage()
    {
        ViewModel = App.GetService<ProjectsViewModel>();
        InitializeComponent();

        Loaded += async (_, _) => await ViewModel.LoadCommand.ExecuteAsync(null);
    }
}
