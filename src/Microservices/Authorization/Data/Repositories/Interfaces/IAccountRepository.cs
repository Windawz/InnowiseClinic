using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

public interface IAccountRepository : IAsyncRepository<AccountEntity>
{
    Task<AccountEntity?> GetAsync(string email);
}