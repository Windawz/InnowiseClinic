using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

public interface IRefreshTokenMapperService
{
    RefreshToken MapFromRefreshTokenEntity(RefreshTokenEntity entity);
    RefreshTokenEntity MapToRefreshTokenEntity(RefreshToken token);
}