using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Mapping;

public static class RequestMapping
{
    public static (string Email, string Password) ToEmailPassword(LogInRequest request)
    {
        var email = request.Email.Trim();
        var password = request.Password.Trim();
        
        return (email, password);
    }

    public static RefreshToken ToRefreshToken(RefreshRequest request)
    {
        return RefreshTokenStringMapping.ToRefreshToken(request.RefreshToken.Trim());
    }

    public static (string Email, string Password, Role Role) ToEmailPasswordRole(RegisterOtherRequest request)
    {
        return (
            Email: request.Email.Trim(),
            Password: request.Password.Trim(),
            Role: RoleNameMapping.ToRole(request.Role.Trim()));
    }

    public static (string Email, string Password) ToEmailPassword(RegisterRequest request)
    {
        return (
            Email: request.Email.Trim(),
            Password: request.Password.Trim());
    }
}