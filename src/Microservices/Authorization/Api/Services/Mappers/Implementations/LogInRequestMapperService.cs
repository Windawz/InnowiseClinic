using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class LogInRequestMapperService : ILogInRequestMapperService
{
    public (string Email, string Password) MapToEmailAndPassword(LogInRequest request)
    {
        var email = request.Email.Trim();
        var password = request.Password.Trim();
        
        return (email, password);
    }
}
