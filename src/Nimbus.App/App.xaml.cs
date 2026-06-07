using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Nimbus.Core;
using Nimbus.App.Services;
using Nimbus.App.ViewModels;
using Nimbus.App.ViewModels.Tabs;
using Nimbus.App.Views;

namespace Nimbus.App;

public partial class App : Application
{
    /// <summary>The single window hosting the shell.</summary>
    public static MainWindow MainWindow { get; private set; } = null!;

    private static IServiceProvider _services = null!;

    public App()
    {
        InitializeComponent();
        _services = ConfigureServices();
    }

    /// <summary>Service-locator escape hatch used by views to fetch their view-models.</summary>
    public static T GetService<T>() where T : class =>
        _services.GetRequiredService<T>();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        MainWindow.NavigateToShell();
        MainWindow.Activate();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Business-logic layer (lives entirely in Nimbus.Core).
        services.AddNimbusCore();

        // UI-only services.
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IPageService>(_ =>
        {
            var pages = new PageService();
            pages.Register<DashboardViewModel, DashboardPage>();
            pages.Register<ProjectsViewModel, ProjectsPage>();
            pages.Register<AnalyticsViewModel, AnalyticsPage>();
            pages.Register<SettingsViewModel, SettingsPage>();
            return pages;
        });

        // View-models.
        services.AddTransient<ShellViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<ProjectsViewModel>();
        services.AddTransient<AnalyticsViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<OverviewTabViewModel>();
        services.AddTransient<RevenueTabViewModel>();
        services.AddTransient<AudienceTabViewModel>();
        services.AddTransient<PerformanceTabViewModel>();

        return services.BuildServiceProvider();
    }
}
