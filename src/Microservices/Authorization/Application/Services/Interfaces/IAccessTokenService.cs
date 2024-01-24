using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IAccessTokenService
{
    Task<AccessToken> GenerateTokenAsync(Guid accountId, Role role);
}