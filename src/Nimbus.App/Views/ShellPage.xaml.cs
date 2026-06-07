using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Nimbus.App.Services;
using Nimbus.App.ViewModels;

namespace Nimbus.App.Views;

public sealed partial class ShellPage : Page
{
    private readonly INavigationService _navigation;
    private readonly IPageService _pageService;

    public ShellViewModel ViewModel { get; }

    public ShellPage()
    {
        ViewModel = App.GetService<ShellViewModel>();
        _navigation = App.GetService<INavigationService>();
        _pageService = App.GetService<IPageService>();

        InitializeComponent();

        // Make the custom strip the draggable title bar.
        App.MainWindow.SetTitleBar(AppTitleBar);

        // Keep the rail's selection in sync with programmatic navigation.
        _navigation.Navigated += OnNavigated;
    }

    private void NavView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        _navigation.Frame = ContentFrame;
        _navigation.NavigateTo(typeof(DashboardViewModel).FullName!);
    }

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        var key = args.IsSettingsSelected
            ? ViewModel.SettingsKey
            : (args.SelectedItem as NavigationItem)?.Key;

        if (key is not null)
            _navigation.NavigateTo(key);
    }

    private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) =>
        _navigation.GoBack();

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (e.SourcePageType == _pageService.GetPageType(ViewModel.SettingsKey))
        {
            NavView.SelectedItem = NavView.SettingsItem;
            return;
        }

        foreach (var item in ViewModel.NavigationItems)
        {
            if (_pageService.GetPageType(item.Key) == e.SourcePageType)
            {
                NavView.SelectedItem = item;
                return;
            }
        }
    }
}
