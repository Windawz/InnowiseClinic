using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class AccountMapperService(IRoleMapperService roleMapperService) : IAccountMapperService
{
    private readonly IRoleMapperService _roleMapperService = roleMapperService;

    public Account MapFromAccountEntity(AccountEntity entity)
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
            Role: _roleMapperService.MapFromRoleName(entity.Role));
    }

    public AccountEntity MapToAccountEntity(Account account)
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
            Role = _roleMapperService.MapToRoleName(account.Role),
        };
    }
}