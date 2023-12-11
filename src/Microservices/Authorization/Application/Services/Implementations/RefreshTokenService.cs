using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Options;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class RefreshTokenService(
    IRefreshTokenRepository refreshTokenRepository,
    IRefreshTokenMapperService refreshTokenMapperService,
    IOptions<RefreshTokenServiceOptions> options) : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IRefreshTokenMapperService _refreshTokenMapperService = refreshTokenMapperService;
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

        await _refreshTokenRepository.AddAsync(
            _refreshTokenMapperService.MapFromRefreshToken(token));
        
        await _refreshTokenRepository.SaveAsync();

        return token;
    }

    public async Task<RefreshToken> CreateReplacementRefreshTokenAsync(RefreshToken refreshToken)
    {
        if (await IsValidAsync(refreshToken))
        {
            var role = refreshToken.Role;
            await InvalidateAsync(refreshToken);
            
            return await CreateRefreshTokenAsync(role);
        }
        else
        {
            throw new InvalidRefreshTokenException(refreshToken.TokenId);
        }
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(Guid tokenId)
    {
        return _refreshTokenMapperService.MapToRefreshToken(
            await _refreshTokenRepository.GetAsync(tokenId)
                ?? throw new InvalidRefreshTokenException(tokenId));
    }

    private async Task InvalidateAsync(RefreshToken refreshToken)
    {
        await _refreshTokenRepository.DeleteAsync(
            _refreshTokenMapperService.MapFromRefreshToken(refreshToken));
        
        await _refreshTokenRepository.SaveAsync();
    }

    private async Task<bool> IsValidAsync(RefreshToken refreshToken)
    {
        var now = DateTime.UtcNow;
        if (now < refreshToken.CreatedAt || now >= refreshToken.ExpiresAt)
        {
            return false;
        }
        else
        {
            return await _refreshTokenRepository.GetAsync(
                _refreshTokenMapperService.MapFromRefreshToken(refreshToken).Id) is not null;
        }
    }
}
