using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

public interface IAccountRepository : IAsyncRepository<AccountEntity>
{
    Task<AccountEntity?> GetAsync(string email);
}