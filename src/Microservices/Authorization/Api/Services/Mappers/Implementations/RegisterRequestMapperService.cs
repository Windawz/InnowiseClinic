using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RegisterRequestMapperService(
    IPasswordHashingService passwordHashingService,
    IRoleMapperService roleMapperService) : IRegisterRequestMapperService
{
    private readonly IPasswordHashingService _passwordHashingService = passwordHashingService;
    private readonly IRoleMapperService _roleMapperService = roleMapperService;

    public (string Email, string Password, Role Role) MapToEmailPasswordAndRole(RegisterRequest request)
    {
        var email = request.Email.Trim();
        var password = _passwordHashingService.GetHashedPassword(email, request.Password.Trim());
        var role = _roleMapperService.MapFromRoleName(request.Role.Trim());

        return (email, password, role);
    }
}