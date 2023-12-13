using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Implementations;

public class RefreshTokenMapperService(IRoleMapperService roleMapperService) : IRefreshTokenMapperService
{
    public RefreshTokenEntity MapToRefreshTokenEntity(RefreshToken token)
    {
        return new()
        {
            Id = token.TokenId,
            Role = roleMapperService.MapToRoleName(token.Role),
            CreatedAt = token.CreatedAt,
            ExpiresAt = token.ExpiresAt,
        };
    }

    public RefreshToken MapFromRefreshTokenEntity(RefreshTokenEntity entity)
    {
        return new(
            TokenId: entity.Id,
            Role: roleMapperService.MapFromRoleName(entity.Role),
            CreatedAt: entity.CreatedAt,
            ExpiresAt: entity.ExpiresAt);
    }
}