using Nimbus.Core.Data;
using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>
/// Computes the derived analytics figures (trends, projections, shares).
/// The views only ever receive finished numbers from here.
/// </summary>
public sealed class AnalyticsService(MockDataStore store) : IAnalyticsService
{
    public Task<IReadOnlyList<TimeSeriesPoint>> GetRevenueTrendAsync() =>
        Task.FromResult(store.MonthlyRevenue);

    public Task<decimal> GetProjectedAnnualRevenueAsync()
    {
        // Simple run-rate projection: annualise the latest month's revenue.
        var latestMonth = store.MonthlyRevenue[^1].Value;
        return Task.FromResult((decimal)(latestMonth * 12));
    }

    public Task<IReadOnlyList<TrafficSource>> GetTrafficSourcesAsync() =>
        Task.FromResult(store.TrafficSources);

    public Task<IReadOnlyList<ServiceHealth>> GetServiceHealthAsync() =>
        Task.FromResult(store.Services);
}
