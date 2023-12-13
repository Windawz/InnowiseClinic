using InnowiseClinic.Microservices.Authorization.Api.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRefreshTokenStringMapperService
{
    /// <exception cref="InvalidRefreshTokenFormatException"/>
    RefreshToken MapToRefreshToken(string refreshTokenString);
    string MapFromRefreshToken(RefreshToken refreshToken);
}