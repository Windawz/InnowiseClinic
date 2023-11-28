using System.Collections.Immutable;
using System.Linq;

namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Represents a user role.
/// </summary>
/// <param name="Name">The name of the role.</param>
/// <param name="RegisterableRoles">
/// The roles that the user with this account is allowed to register other accounts as.
/// </param>
public record Role
{
    /// <summary>
    /// Role that all patients belong to.
    /// </summary>
    /// <remarks>
    /// May not register accounts of others and so
    /// has no registerable roles.
    /// </remarks>
    public static readonly Role Patient = new(
        RoleNames.Patient,
        Array.Empty<Role>());

    /// <summary>
    /// Role that all doctors belong to.
    /// </summary>
    /// <remarks>
    /// May register new patients, so its registerable roles
    /// include <see cref="Patient"/>.
    /// </remarks>
    public static readonly Role Doctor = new(
        RoleNames.Doctor,
        new[] { Patient });

    /// <summary>
    /// Role that all receptionists belong to.
    /// </summary>
    /// <remarks>
    /// May register new patients and doctors, so its registerable roles
    /// include <see cref="Patient"/> and <see cref="Doctor"/>.
    /// </remarks>
    public static readonly Role Receptionist = new(
        RoleNames.Receptionist,
        new Role[] { Patient, Doctor });

    private static readonly IReadOnlyDictionary<string, Role> _knownRoles;

    static Role()
    {
        _knownRoles = Enumerable.Empty<Role>()
            .Append(Patient)
            .Append(Doctor)
            .Append(Receptionist)
            .ToDictionary(
                role => role.Name,
                role => role,
                new RoleNameEqualityComparer());
    }

    private Role(string name, IReadOnlyCollection<Role> registerableRoles)
    {
        Name = name;
        RegisterableRoles = registerableRoles;
    }

    /// <summary>
    /// Role name.
    /// </summary>
    public string Name { get; private init; }

    /// <summary>
    /// Roles that a user belonging to this role can assign to an account being registered by them.
    /// </summary>
    public IReadOnlyCollection<Role> RegisterableRoles { get; private init; }

    /// <summary>
    /// Tries to provide an existing role from the name.
    /// </summary>
    /// <param name="roleName">Name of an existing role.</param>
    /// <param name="role">Existing role to be provided if one exists with such a name.</param>
    /// <returns>
    /// True if a role with the provided name exists, false otherwise.
    /// </returns>
    /// <remarks>
    /// The value of <paramref name="roleName"/> will be trimmed and compared ignoring the case and the culture.
    /// </remarks>
    public static bool TryParse(string roleName, out Role role)
    {
        return _knownRoles.TryGetValue(roleName, out role!);
    }

    /// <summary>
    /// Tries to provide an existing role from the name.
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns>Existing role with the provided name.</returns>
    /// <remarks>
    /// Throws <see cref="UnknownRoleNameException"/> if
    /// no existing role with the provided name was found.
    /// </remarks>
    /// <exception cref="UnknownRoleNameException">
    /// No existing role with the provided name was found.
    /// </exception>
    public static Role Parse(string roleName)
    {
        if (TryParse(roleName, out Role role))
        {
            return role;
        }
        else
        {
            throw new UnknownRoleNameException(roleName);
        }
    }
}