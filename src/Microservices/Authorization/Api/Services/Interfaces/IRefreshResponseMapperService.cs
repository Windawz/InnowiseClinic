using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Responses;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IRefreshResponseMapperService
{
    RefreshResponse MapToRefreshResponse(RefreshToken refreshToken);
}