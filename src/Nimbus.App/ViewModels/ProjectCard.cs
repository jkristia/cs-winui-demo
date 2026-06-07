using System.Globalization;
using Nimbus.Core.Models;

namespace Nimbus.App.ViewModels;

/// <summary>
/// View-side projection that pairs a project with its resolved team and exposes
/// pre-formatted display strings, so the view never formats anything itself.
/// </summary>
public sealed record ProjectCard(Project Project, IReadOnlyList<TeamMember> Team)
{
    private static readonly CultureInfo Usd = CultureInfo.GetCultureInfo("en-US");

    public string DueText => $"Due {Project.DueDate.ToString("MMM d, yyyy", Usd)}";

    public string BudgetText => Project.Budget.ToString("C0", Usd);
}
