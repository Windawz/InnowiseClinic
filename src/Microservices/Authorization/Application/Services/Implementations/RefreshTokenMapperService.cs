using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class RefreshTokenMapperService : IRefreshTokenMapperService
{
    public RefreshTokenEntity MapFromRefreshToken(RefreshToken token)
    {
        return new()
        {
            Id = token.TokenId,
            CreatedAt = token.CreatedAt,
            ExpiresAt = token.ExpiresAt,
        };
    }

    public RefreshToken MapToRefreshToken(RefreshTokenEntity entity)
    {
        return new(
            TokenId: entity.Id,
            CreatedAt: entity.CreatedAt,
            ExpiresAt: entity.ExpiresAt);
    }
}