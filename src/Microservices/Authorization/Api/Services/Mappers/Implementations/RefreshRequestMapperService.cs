using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RefreshRequestMapperService(
    IRefreshTokenStringMapperService refreshTokenStringMapperService) : IRefreshRequestMapperService
{
    public RefreshToken MapToRefreshToken(RefreshRequest request)
    {
        return refreshTokenStringMapperService.MapToRefreshToken(
            request.RefreshToken.Trim());
    }
}