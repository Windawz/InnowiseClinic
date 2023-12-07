using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly AuthorizationDbContext _dbContext;

    public AccountRepository(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Account entity)
    {
        await _dbContext.Accounts.AddAsync(entity);
    }

    public async Task DeleteAsync(Account entity)
    {
        _dbContext.Accounts.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Account?> GetAsync(string email)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(account => account.Email
                .Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Account?> GetAsync(Guid id)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(account => account.Id.Equals(id));
    }

    public async Task UpdateAsync(Account entity)
    {
        _dbContext.Update(entity);
        await Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}