using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IRefreshRequestMapperService
{
    RefreshToken MapFromRefreshRequest(RefreshRequest request);
}