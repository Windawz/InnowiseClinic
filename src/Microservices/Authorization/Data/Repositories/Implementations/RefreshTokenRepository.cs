using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class RefreshTokenRepository : AsyncRepository<RefreshTokenEntity, AuthorizationDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AuthorizationDbContext dbContext) : base(dbContext) { }
}