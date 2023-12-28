using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Options;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRefreshTokenMapperService _refreshTokenMapperService;
    private readonly RefreshTokenServiceOptions _options;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IRefreshTokenMapperService refreshTokenMapperService,
        IOptions<RefreshTokenServiceOptions> options)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _refreshTokenMapperService = refreshTokenMapperService;
        _options = options.Value;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(Role role)
    {
        var now = DateTime.UtcNow;
        var id = Guid.NewGuid();

        var token = new RefreshToken(
            TokenId: id,
            Role: role,
            CreatedAt: now,
            ExpiresAt: now.AddSeconds(_options.ExpirationSeconds));

        await _refreshTokenRepository.AddAsync(
            _refreshTokenMapperService.MapToRefreshTokenEntity(token));
        
        await _refreshTokenRepository.SaveAsync();

        return token;
    }

    public async Task<RefreshToken> CreateReplacementRefreshTokenAsync(RefreshToken refreshToken)
    {
        var now = DateTime.UtcNow;
        RefreshToken? replacementRefreshToken = null;

        if (IsValid(refreshToken, now))
        {
            var id = _refreshTokenMapperService.MapToRefreshTokenEntity(refreshToken).Id;
            var entity = await _refreshTokenRepository.GetAsync(id);
            if (entity is not null)
            {
                _refreshTokenRepository.Delete(entity);
                await _refreshTokenRepository.SaveAsync();
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
