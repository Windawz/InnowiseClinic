using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class RefreshTokenRepository(AuthorizationDbContext dbContext)
    : AsyncRepository<RefreshTokenEntity>(dbContext), IRefreshTokenRepository { }