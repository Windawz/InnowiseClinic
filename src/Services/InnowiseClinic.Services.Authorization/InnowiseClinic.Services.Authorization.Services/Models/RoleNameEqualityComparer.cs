using System.Diagnostics.CodeAnalysis;

namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Used for comparing names of roles.
/// </summary>
/// <remarks>
/// Role name comparison and hash code generation trims the operands and is ordinal and case-insensitive.
/// </remarks>
internal class RoleNameEqualityComparer : IEqualityComparer<string>
{
    /// <summary>
    /// Compares two role names for equality.
    /// </summary>
    /// <param name="x">Role name.</param>
    /// <param name="y">Another role name.</param>
    /// <returns>True if the role names are equal, false otherwise.</returns>
    /// <remarks>
    /// Role name comparison trims the operands and is ordinal and case-insensitive.
    /// </remarks>
    public bool Equals(string? x, string? y)
    {
        return string.Equals(x?.Trim(), y?.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the hash code of the role name.
    /// </summary>
    /// <param name="obj">Role name.</param>
    /// <returns>
    /// The hash code of the role name.
    /// </returns>
    /// /// <remarks>
    /// Role name hash code generation trims the operands and is ordinal and case-insensitive.
    /// </remarks>
    public int GetHashCode([DisallowNull] string obj)
    {
        return string.GetHashCode(obj.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}