using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRefreshTokenStringMapperService
{
    RefreshToken MapToRefreshToken(string refreshTokenString);
    string MapFromRefreshToken(RefreshToken refreshToken);
}