using Nimbus.Core.Data;
using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>Filtering and team-resolution logic for the project portfolio.</summary>
public sealed class ProjectService(MockDataStore store) : IProjectService
{
    public Task<IReadOnlyList<Project>> GetProjectsAsync(ProjectStatus? status = null)
    {
        IEnumerable<Project> query = store.Projects;

        if (status is { } s)
            query = query.Where(p => p.Status == s);

        IReadOnlyList<Project> result = query
            .OrderBy(p => p.Status)
            .ThenBy(p => p.DueDate)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<TeamMember>> GetTeamAsync(Project project)
    {
        IReadOnlyList<TeamMember> members = store.TeamMembers
            .Where(m => project.TeamMemberIds.Contains(m.Id))
            .ToList();

        return Task.FromResult(members);
    }
}
