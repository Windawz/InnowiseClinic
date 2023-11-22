using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// Searches for roles by various criteria.
/// </summary>
public interface IRoleResolver
{
    /// <summary>
    /// Searches for a role with a specific name.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <returns>
    /// The role if found, null otherwise.
    /// </returns>
    Role? ResolveByName(string name);
}