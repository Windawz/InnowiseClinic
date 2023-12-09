using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Options;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
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

    public async Task<RefreshToken> GenerateRefreshTokenAsync()
    {
        var now = DateTime.UtcNow;
        var id = Guid.NewGuid();

        var token = new RefreshToken(
            CreatedAt: now,
            ExpiresAt: now.AddSeconds(_options.ExpirationSeconds),
            TokenId: id);

        await _refreshTokenRepository.AddAsync(
            _refreshTokenMapperService.MapFromRefreshToken(token));
        
        await _refreshTokenRepository.SaveAsync();

        return token;
    }

    public async Task InvalidateAsync(RefreshToken refreshToken)
    {
        await _refreshTokenRepository.DeleteAsync(
            _refreshTokenMapperService.MapFromRefreshToken(refreshToken));
        
        await _refreshTokenRepository.SaveAsync();
    }

    public async Task<bool> IsValidAsync(RefreshToken refreshToken)
    {
        return await _refreshTokenRepository.GetAsync(
            _refreshTokenMapperService.MapFromRefreshToken(refreshToken).Id) is not null;
    }
}
