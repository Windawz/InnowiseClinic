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
            Email = serviceAccount.Email.Address,
            Password = serviceAccount.Password,
            IsEmailVerified = serviceAccount.IsEmailVerified,
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
            dataAccount.Id,
            new Email(dataAccount.Email),
            dataAccount.Password,
            dataAccount.Roles
                .Select(role => FromDataRole(role))
                .ToArray())
        {
            IsEmailVerified = dataAccount.IsEmailVerified,
            CreatedBy = dataAccount.CreatedBy is not null
                ? FromDataAccount(dataAccount.CreatedBy)
                : null,
            CreatedAt = dataAccount.CreatedAt,
            UpdatedBy = dataAccount.UpdatedBy is not null
                ? FromDataAccount(dataAccount.UpdatedBy)
                : null,
            UpdatedAt = dataAccount.UpdatedAt,
        };
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
            Id = default,
            Name = serviceRole.Name,
        };
    }

    /// <summary>
    /// Converts a data layer role to a service layer role.
    /// </summary>
    /// <param name="dataRole">A data layer role.</param>
    /// <returns>A service layer role.</returns>
    public static Role FromDataRole(Data.Models.Role dataRole)
    {
        return Role.Parse(dataRole.Name);
    }
}