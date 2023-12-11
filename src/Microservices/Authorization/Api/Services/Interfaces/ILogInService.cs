using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface ILogInService
{
    Task<TokenResponse> LogInAsync(LogInRequest request);
}