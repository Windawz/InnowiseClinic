using System.Collections.Immutable;

namespace InnowiseClinic.Services.Authorization.Services.Models;

public static class Mapping
{
    public static Data.Models.Account ToDataAccount(this Account serviceAccount)
    {
        return new()
        {
            Id = serviceAccount.Id,
            Email = serviceAccount.Email.Address,
            Password = serviceAccount.Password.Text,
            IsEmailVerified = serviceAccount.IsEmailVerified,
            CreatedBy = serviceAccount.CreatedBy?.ToDataAccount(),
            CreatedAt = serviceAccount.CreatedAt,
            UpdatedBy = serviceAccount.UpdatedBy?.ToDataAccount(),
            UpdatedAt = serviceAccount.UpdatedAt,
            Role = serviceAccount.Role.Name
        };
    }

    public static Account ToServiceAccount(this Data.Models.Account dataAccount)
    {
        return new(
            dataAccount.Id,
            new Email(dataAccount.Email),
            new Password(dataAccount.Password),
            new Role(dataAccount.Role))
        {
            IsEmailVerified = dataAccount.IsEmailVerified,
            CreatedBy = dataAccount.CreatedBy?.ToServiceAccount(),
            CreatedAt = dataAccount.CreatedAt,
            UpdatedBy = dataAccount.UpdatedBy?.ToServiceAccount(),
            UpdatedAt = dataAccount.UpdatedAt,
        };
    }
}