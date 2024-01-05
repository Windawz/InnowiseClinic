using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Mapping;

public static class AccountMapping
{
    public static Account ToAccount(AccountEntity entity)
    {
        return new(
            Id: entity.Id,
            Email: entity.Email,
            Password: entity.Password,
            IsEmailVerified: entity.IsEmailVerified,
            CreatedByEmail: entity.CreatedByEmail,
            CreatedAt: entity.CreatedAt,
            UpdatedByEmail: entity.UpdatedByEmail,
            UpdatedAt: entity.UpdatedAt,
            Role: RoleMapping.ToRole(entity.Role));
    }

    public static AccountEntity ToAccountEntity(Account account)
    {
        return new()
        {
            Id = account.Id,
            Email = account.Email,
            Password = account.Password,
            IsEmailVerified = account.IsEmailVerified,
            CreatedByEmail = account.CreatedByEmail,
            CreatedAt = account.CreatedAt,
            UpdatedByEmail = account.UpdatedByEmail,
            UpdatedAt = account.UpdatedAt,
            Role = RoleMapping.ToRoleName(account.Role),
        };
    }
}