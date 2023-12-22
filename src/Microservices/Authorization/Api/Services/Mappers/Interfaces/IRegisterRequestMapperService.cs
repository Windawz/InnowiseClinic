using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRegisterRequestMapperService
{
    (string Email, string Password) MapToEmailPassword(RegisterRequest request);
}