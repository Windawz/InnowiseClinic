using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;

public interface IRegisterOtherRequestMapperService
{
    (string Email, string Password, Role Role) MapToEmailPasswordRole(RegisterOtherRequest request);
}