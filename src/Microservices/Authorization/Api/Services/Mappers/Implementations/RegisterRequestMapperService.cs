using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RegisterRequestMapperService : IRegisterRequestMapperService
{
    public (string Email, string Password) MapToEmailPassword(RegisterRequest request)
    {
        return (
            Email: request.Email.Trim(),
            Password: request.Password.Trim());
    }
}