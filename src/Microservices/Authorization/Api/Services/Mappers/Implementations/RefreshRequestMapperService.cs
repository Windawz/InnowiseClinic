using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RefreshRequestMapperService : IRefreshRequestMapperService
{
    private readonly IRefreshTokenStringMapperService _refreshTokenStringMapperService;

    public RefreshRequestMapperService(IRefreshTokenStringMapperService refreshTokenStringMapperService)
    {
        _refreshTokenStringMapperService = refreshTokenStringMapperService;
    }

    public RefreshToken MapToRefreshToken(RefreshRequest request)
    {
        return _refreshTokenStringMapperService.MapToRefreshToken(
            request.RefreshToken.Trim());
    }
}