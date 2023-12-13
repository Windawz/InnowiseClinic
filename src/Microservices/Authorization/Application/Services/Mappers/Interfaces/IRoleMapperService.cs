using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

public interface IRoleMapperService
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    string MapToRoleName(Role role);

    /// <exception cref="ArgumentException"/>
    Role MapFromRoleName(string roleName);
}