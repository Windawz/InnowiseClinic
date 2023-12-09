using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IRefreshTokenMapperService
{
    RefreshToken MapToRefreshToken(RefreshTokenEntity entity);
    RefreshTokenEntity MapFromRefreshToken(RefreshToken token);
}