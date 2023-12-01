using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public static class Mapping
{
    public static Data.Models.Account MapToDataLayer(this Account serviceAccount)
    {
        return new()
        {
            Id = serviceAccount.Id,
            Email = serviceAccount.Email.Address,
            Password = serviceAccount.Password.Text,
            IsEmailVerified = serviceAccount.IsEmailVerified,
            CreatedById = serviceAccount.CreatedBy?.Id,
            CreatedAt = serviceAccount.CreatedAt,
            UpdatedById = serviceAccount.UpdatedBy?.Id,
            UpdatedAt = serviceAccount.UpdatedAt,
            Role = serviceAccount.Role.Name
        };
    }

    public static Account MapToServiceLayer(this Data.Models.Account dataAccount)
    {
        return new(
            dataAccount.Id,
            new Email(dataAccount.Email),
            new Password(dataAccount.Password),
            new Role(dataAccount.Role))
        {
            IsEmailVerified = dataAccount.IsEmailVerified,
            CreatedBy = dataAccount.CreatedBy?.MapToServiceLayer(),
            CreatedAt = dataAccount.CreatedAt,
            UpdatedBy = dataAccount.UpdatedBy?.MapToServiceLayer(),
            UpdatedAt = dataAccount.UpdatedAt,
        };
    }
}