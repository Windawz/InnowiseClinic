using InnowiseClinic.Microservices.Shared.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;

public abstract class AsyncRepository<TEntity, TContext>(TContext dbContext) : IAsyncRepository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    protected TContext DbContext { get; } = dbContext;

    public async virtual Task<TEntity?> GetAsync(Guid id)
    {
        return await DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async virtual Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>()
            .AddAsync(entity);
    }

    public async virtual Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>()
            .Update(entity);
        
        await Task.CompletedTask;
    }

    public async virtual Task DeleteAsync(TEntity entity)
    {
        DbContext.Set<TEntity>()
            .Remove(entity);

        await Task.CompletedTask;
    }

    public async virtual Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}