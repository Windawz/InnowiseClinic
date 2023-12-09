using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface ILogInResponseMapperService
{
    LogInResponse MapToLogInResponse(AccessToken accessToken, RefreshToken refreshToken);
}