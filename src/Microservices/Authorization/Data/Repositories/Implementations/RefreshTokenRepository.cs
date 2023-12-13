using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;

public class RefreshTokenRepository(AuthorizationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshTokenEntity entity)
    {
        await dbContext.RefreshTokens.AddAsync(entity);
    }

    public async Task DeleteAsync(RefreshTokenEntity entity)
    {
        dbContext.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<RefreshTokenEntity?> GetAsync(Guid id)
    {
        return await dbContext.RefreshTokens.FirstOrDefaultAsync(
            entity => entity.Id == id);
    }

    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshTokenEntity entity)
    {
        dbContext.Update(entity);
        await Task.CompletedTask;
    }
}