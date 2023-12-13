using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class AccountRepository(AuthorizationDbContext dbContext)
    : AsyncRepository<AccountEntity>(dbContext), IAccountRepository
{
    public async Task<AccountEntity?> GetAsync(string email)
    {
        return await DbContext.Accounts
            .FirstOrDefaultAsync(account => EF.Functions.Like(account.Email, email));
    }
}