using Nimbus.Core.Models;

namespace Nimbus.Core.Services;

/// <summary>Read access and querying over the project portfolio.</summary>
public interface IProjectService
{
    Task<IReadOnlyList<Project>> GetProjectsAsync(ProjectStatus? status = null);
    Task<IReadOnlyList<TeamMember>> GetTeamAsync(Project project);
}
