using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Represents a user role.
/// </summary>
/// <param name="Id">A unique identifier.</param>
/// <param name="Name">The name of the role.</param>
/// <param name="RegisterableRoles">
/// The roles that the user with this account is allowed to register other accounts as.
/// </param>
public record Role(
    int Id,
    string Name,
    IReadOnlyCollection<Role> RegisterableRoles);