using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AuthorizationDbContext _dbContext;

    public RefreshTokenRepository(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RefreshTokenEntity entity)
    {
        await _dbContext.RefreshTokens.AddAsync(entity);
    }

    public async Task DeleteAsync(RefreshTokenEntity entity)
    {
        _dbContext.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<RefreshTokenEntity?> GetAsync(Guid id)
    {
        return await _dbContext.RefreshTokens.FirstOrDefaultAsync(
            entity => entity.Id == id);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshTokenEntity entity)
    {
        _dbContext.Update(entity);
        await Task.CompletedTask;
    }
}