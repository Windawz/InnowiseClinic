using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface ILogInResponseMapperService
{
    TokenResponse MapToLogInResponse(AccessToken accessToken, RefreshToken refreshToken);
}