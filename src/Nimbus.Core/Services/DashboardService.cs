using System.Globalization;
using Nimbus.Core.Data;
using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>
/// Derives dashboard KPIs from the raw project / revenue data.
/// All of the "what counts as active", "how do we format money", and
/// "what is the trend" decisions live here — never in the views.
/// </summary>
public sealed class DashboardService(MockDataStore store) : IDashboardService
{
    private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

    public Task<IReadOnlyList<Metric>> GetKpisAsync()
    {
        var active = store.Projects.Count(p => p.Status == ProjectStatus.Active);
        var totalBudget = store.Projects
            .Where(p => p.Status is ProjectStatus.Active or ProjectStatus.Planning)
            .Sum(p => p.Budget);

        var revenue = store.MonthlyRevenue;
        var latest = revenue[^1].Value;
        var previous = revenue[^2].Value;
        var revenueChange = (latest - previous) / previous * 100.0;

        var avgProgress = store.Projects
            .Where(p => p.Status == ProjectStatus.Active)
            .Average(p => p.Progress);

        IReadOnlyList<Metric> kpis =
        [
            new() { Label = "Active Projects", Value = active.ToString(), Glyph = "",
                    ChangePercent = 12.5 },
            new() { Label = "Monthly Revenue", Value = latest.ToString("C0", Usd), Glyph = "",
                    ChangePercent = Math.Round(revenueChange, 1) },
            new() { Label = "Committed Budget", Value = totalBudget.ToString("C0", Usd), Glyph = "",
                    ChangePercent = 8.3 },
            new() { Label = "Avg. Completion", Value = avgProgress.ToString("P0", Usd), Glyph = "",
                    ChangePercent = -2.1 },
        ];

        return Task.FromResult(kpis);
    }

    public Task<IReadOnlyList<Project>> GetActiveProjectsAsync(int take = 4)
    {
        IReadOnlyList<Project> result = store.Projects
            .Where(p => p.Status == ProjectStatus.Active)
            .OrderByDescending(p => p.Progress)
            .Take(take)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<ActivityItem>> GetRecentActivityAsync(int take = 5)
    {
        IReadOnlyList<ActivityItem> result = store.Activity
            .OrderByDescending(a => a.Timestamp)
            .Take(take)
            .ToList();

        return Task.FromResult(result);
    }
}
