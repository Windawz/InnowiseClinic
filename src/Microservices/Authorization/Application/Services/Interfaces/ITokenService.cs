using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface ITokenService
{
    Task<AccessToken> GenerateAccessTokenAsync(Role role);
    Task<RefreshToken> GenerateRefreshTokenAsync();
    Task<bool> IsValidAsync(RefreshToken refreshToken);
    Task InvalidateAsync(RefreshToken refreshToken);
}