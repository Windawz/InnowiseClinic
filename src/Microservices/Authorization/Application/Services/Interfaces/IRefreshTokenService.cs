using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(Role role);
    Task<RefreshToken> CreateReplacementRefreshTokenAsync(RefreshToken refreshToken);
}