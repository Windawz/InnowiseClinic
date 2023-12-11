using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class LogInRequestMapperService(IPasswordHashingService passwordHashingService) : ILogInRequestMapperService
{
    private readonly IPasswordHashingService _passwordHashingService = passwordHashingService;

    public (string Email, string Password) MapToEmailAndPassword(LogInRequest request)
    {
        var email = request.Email.Trim();
        var password = _passwordHashingService.GetHashedPassword(
            email,
            _passwordHashingService.GetHashedPassword(email, request.Password));
        
        return (email, password);
    }
}
