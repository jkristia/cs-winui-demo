using Microsoft.UI.Xaml.Controls;
using Nimbus.App.ViewModels;

namespace Nimbus.App.Views;

public sealed partial class DashboardPage : Page
{
    public DashboardViewModel ViewModel { get; }

    public DashboardPage()
    {
        ViewModel = App.GetService<DashboardViewModel>();
        InitializeComponent();

        Loaded += async (_, _) => await ViewModel.LoadCommand.ExecuteAsync(null);
    }
}
