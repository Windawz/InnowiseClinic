using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IRegisterOtherService
{
    Task RegisterOther(RegisterOtherRequest request);
}