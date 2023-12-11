using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface ILogInRequestMapperService
{
    (string Email, string Password) MapToEmailAndPassword(LogInRequest request);
}