using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRoleNameMapperService
{
    Role MapToRole(string roleName);
    string MapFromRole(Role role);
}