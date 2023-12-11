using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRegisterRequestMapperService
{
    (string Email, string Password, Role Role) MapToEmailPasswordAndRole(RegisterRequest request);
}