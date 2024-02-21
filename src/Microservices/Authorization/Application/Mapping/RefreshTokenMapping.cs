using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using Microsoft.Identity.Client;

namespace InnowiseClinic.Microservices.Authorization.Application.Mapping;

public static class RefreshTokenMapping
{
    public static RefreshTokenEntity ToRefreshTokenEntity(RefreshToken token)
    {
        return new()
        {
            Id = token.TokenId,
            AccountId = token.AccountId,
            Role = RoleMapping.ToRoleName(token.Role),
            CreatedAt = token.CreatedAt,
            ExpiresAt = token.ExpiresAt,
        };
    }

    public static RefreshToken ToRefreshToken(RefreshTokenEntity entity)
    {
        return new(
            TokenId: entity.Id,
            AccountId: entity.AccountId,
            Role: RoleMapping.ToRole(entity.Role),
            CreatedAt: entity.CreatedAt,
            ExpiresAt: entity.ExpiresAt);
    }
}