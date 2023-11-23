using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Contains methods that map models from different layers to each other.
/// </summary>
public static class Mapping
{
    /// <summary>
    /// Converts a service layer account to a data layer account.
    /// </summary>
    /// <param name="serviceAccount">A service layer account.</param>
    /// <returns>A data layer account.</returns>
    public static Data.Models.Account ToDataAccount(Account serviceAccount)
    {
        return new()
        {
            Id = serviceAccount.Id,
            Email = serviceAccount.Email,
            Password = serviceAccount.Password,
            PhoneNumber = serviceAccount.PhoneNumber,
            IsEmailVerified = serviceAccount.IsEmailVerified,
            PhotoId = serviceAccount.PhotoId,
            CreatedBy = serviceAccount.CreatedBy is not null
                ? ToDataAccount(serviceAccount.CreatedBy)
                : null,
            CreatedAt = serviceAccount.CreatedAt,
            UpdatedBy = serviceAccount.UpdatedBy is not null
                ? ToDataAccount(serviceAccount.UpdatedBy)
                : null,
            UpdatedAt = serviceAccount.UpdatedAt,
            Roles = serviceAccount.Roles
                .Select(role => ToDataRole(role))
                .ToList(),
        };
    }

    /// <summary>
    /// Converts a data layer account to a service layer account.
    /// </summary>
    /// <param name="dataAccount">A data layer account.</param>
    /// <returns>A service layer account.</returns>
    public static Account FromDataAccount(Data.Models.Account dataAccount)
    {
        return new(
            Id: dataAccount.Id,
            Email: dataAccount.Email,
            Password: dataAccount.Password,
            PhoneNumber: dataAccount.PhoneNumber,
            IsEmailVerified: dataAccount.IsEmailVerified,
            PhotoId: dataAccount.PhotoId,
            CreatedBy: dataAccount.CreatedBy is not null
                ? FromDataAccount(dataAccount.CreatedBy)
                : null,
            CreatedAt: dataAccount.CreatedAt,
            UpdatedBy: dataAccount.UpdatedBy is not null
                ? FromDataAccount(dataAccount.UpdatedBy)
                : null,
            UpdatedAt: dataAccount.UpdatedAt,
            Roles: dataAccount.Roles
                .Select(role => FromDataRole(role))
                .ToArray());
    }

    /// <summary>
    /// Converts a service layer role to a data layer role.
    /// </summary>
    /// <param name="serviceRole">A service layer role.</param>
    /// <returns>A data layer role.</returns>
    public static Data.Models.Role ToDataRole(Role serviceRole)
    {
        return new()
        {
            Id = serviceRole.Id,
            Name = serviceRole.Name,
            RegisterableRoles = serviceRole.RegisterableRoles
                .Select(role => ToDataRole(role))
                .ToList(),
        };
    }

    /// <summary>
    /// Converts a data layer role to a service layer role.
    /// </summary>
    /// <param name="dataRole">A data layer role.</param>
    /// <returns>A service layer role.</returns>
    public static Role FromDataRole(Data.Models.Role dataRole)
    {
        return new(
            Id: dataRole.Id,
            Name: dataRole.Name,
            RegisterableRoles: dataRole.RegisterableRoles
                .Select(role => FromDataRole(role))
                .ToArray());
    }
}