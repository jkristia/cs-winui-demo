namespace Nimbus.Core.Models;

/// <summary>A person who can be assigned to projects.</summary>
public sealed record TeamMember
{
    public string Id { get; init; } = "";
    public string Name { get; init; } = "";
    public string Role { get; init; } = "";

    /// <summary>Two-letter monogram used by the UI as an avatar fallback.</summary>
    public string Initials =>
        string.Concat(Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                          .Take(2)
                          .Select(part => part[0]));
}
