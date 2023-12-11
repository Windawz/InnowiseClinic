using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(Role role);
    
    /// <exception cref="InvalidRefreshTokenException"/>
    Task<RefreshToken> CreateReplacementRefreshTokenAsync(RefreshToken refreshToken);
}