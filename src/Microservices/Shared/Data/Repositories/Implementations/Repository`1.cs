using InnowiseClinic.Microservices.Shared.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;

public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : Entity
    where TContext : DbContext
{
    public Repository(TContext dbContext)
    {
        DbContext = dbContext;
    }

    protected TContext DbContext { get; }

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

    public virtual void Update(TEntity entity)
    {
        DbContext.Set<TEntity>()
            .Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>()
            .Remove(entity);
    }

    public async virtual Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}