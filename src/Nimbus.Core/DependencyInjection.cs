using Microsoft.Extensions.DependencyInjection;
using Nimbus.Core.Data;
using Nimbus.Core.Services;

namespace Nimbus.Core;

/// <summary>
/// Registers the business-logic layer with the DI container so the UI
/// project never has to know how these services are constructed.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddNimbusCore(this IServiceCollection services)
    {
        services.AddSingleton<MockDataStore>();
        services.AddSingleton<IDashboardService, DashboardService>();
        services.AddSingleton<IProjectService, ProjectService>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();
        return services;
    }
}
