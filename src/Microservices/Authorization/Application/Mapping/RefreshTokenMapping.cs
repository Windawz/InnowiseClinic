using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Mapping;

public static class RefreshTokenMapping
{
    public static RefreshTokenEntity ToRefreshTokenEntity(RefreshToken token)
    {
        return new()
        {
            Id = token.TokenId,
            Role = RoleMapping.ToRoleName(token.Role),
            CreatedAt = token.CreatedAt,
            ExpiresAt = token.ExpiresAt,
        };
    }

    public static RefreshToken ToRefreshToken(RefreshTokenEntity entity)
    {
        return new(
            TokenId: entity.Id,
            Role: RoleMapping.ToRole(entity.Role),
            CreatedAt: entity.CreatedAt,
            ExpiresAt: entity.ExpiresAt);
    }
}