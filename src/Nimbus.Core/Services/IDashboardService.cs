using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>Provides the aggregated figures shown on the landing dashboard.</summary>
public interface IDashboardService
{
    Task<IReadOnlyList<Metric>> GetKpisAsync();
    Task<IReadOnlyList<Project>> GetActiveProjectsAsync(int take = 4);
    Task<IReadOnlyList<ActivityItem>> GetRecentActivityAsync(int take = 5);
}
