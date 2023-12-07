using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

public interface IAccountRepository : IAsyncRepository<Account>
{
    Task<Account?> GetAsync(string email);
}