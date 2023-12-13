using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Options;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class RefreshTokenService(
    IRefreshTokenRepository refreshTokenRepository,
    IRefreshTokenMapperService refreshTokenMapperService,
    IOptions<RefreshTokenServiceOptions> options) : IRefreshTokenService
{
    private readonly RefreshTokenServiceOptions _options = options.Value;

    public async Task<RefreshToken> CreateRefreshTokenAsync(Role role)
    {
        var now = DateTime.UtcNow;
        var id = Guid.NewGuid();

        var token = new RefreshToken(
            TokenId: id,
            Role: role,
            CreatedAt: now,
            ExpiresAt: now.AddSeconds(_options.ExpirationSeconds));

        await refreshTokenRepository.AddAsync(
            refreshTokenMapperService.MapToRefreshTokenEntity(token));
        
        await refreshTokenRepository.SaveAsync();

        return token;
    }

    public async Task<RefreshToken> CreateReplacementRefreshTokenAsync(RefreshToken refreshToken)
    {
        var now = DateTime.UtcNow;
        RefreshToken? replacementRefreshToken = null;

        if (IsValid(refreshToken, now))
        {
            var id = refreshTokenMapperService.MapToRefreshTokenEntity(refreshToken).Id;
            var entity = await refreshTokenRepository.GetAsync(id);
            if (entity is not null)
            {
                await refreshTokenRepository.DeleteAsync(entity);
                await refreshTokenRepository.SaveAsync();
                replacementRefreshToken = await CreateRefreshTokenAsync(refreshToken.Role);
            }
        }

        if (replacementRefreshToken is null)
        {
            throw new InvalidRefreshTokenException(refreshToken.TokenId);
        }
        else
        {
            return replacementRefreshToken;
        }
    }

    private static bool IsValid(RefreshToken refreshToken, DateTime currentDateTime)
    {
        return currentDateTime >= refreshToken.CreatedAt && currentDateTime < refreshToken.ExpiresAt;
    }
}
