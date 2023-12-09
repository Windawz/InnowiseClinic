using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshTokenEntity> { }