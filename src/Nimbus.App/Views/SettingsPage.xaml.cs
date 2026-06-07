using Microsoft.UI.Xaml.Controls;
using Nimbus.App.ViewModels;

namespace Nimbus.App.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }
}
