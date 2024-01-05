using InnowiseClinic.Microservices.Authorization.Application.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(Role role);
    
    /// <exception cref="InvalidRefreshTokenException"/>
    Task<RefreshToken> RecreateRefreshTokenAsync(RefreshToken refreshToken);
}