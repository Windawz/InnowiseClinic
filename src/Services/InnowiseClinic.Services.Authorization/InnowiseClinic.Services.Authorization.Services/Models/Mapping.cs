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
            Password = serviceAccount.Password.Text,
            IsEmailVerified = serviceAccount.IsEmailVerified,
            CreatedBy = serviceAccount.CreatedBy is not null
                ? ToDataAccount(serviceAccount.CreatedBy)
                : null,
            CreatedAt = serviceAccount.CreatedAt,
            UpdatedBy = serviceAccount.UpdatedBy is not null
                ? ToDataAccount(serviceAccount.UpdatedBy)
                : null,
            UpdatedAt = serviceAccount.UpdatedAt,
            Role = serviceAccount.Role.Name
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
            new Password(dataAccount.Password),
            new Role(dataAccount.Role))
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
}