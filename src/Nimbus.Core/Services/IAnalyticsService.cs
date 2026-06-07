using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>Supplies the data series rendered across the analytics tabs.</summary>
public interface IAnalyticsService
{
    Task<IReadOnlyList<TimeSeriesPoint>> GetRevenueTrendAsync();
    Task<decimal> GetProjectedAnnualRevenueAsync();
    Task<IReadOnlyList<TrafficSource>> GetTrafficSourcesAsync();
    Task<IReadOnlyList<ServiceHealth>> GetServiceHealthAsync();
}
