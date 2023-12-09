using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IRegisterService
{
    Task RegisterAsync(RegisterRequest request);
}