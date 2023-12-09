using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IRefreshService
{
    Task<RefreshResponse> RefreshAsync(RefreshRequest request);
}