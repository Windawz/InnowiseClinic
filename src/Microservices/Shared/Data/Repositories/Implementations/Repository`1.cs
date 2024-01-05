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
        var set = DbContext.Set<TEntity>();

        if (GetTrackedEntity(entity) is TEntity trackedEntity)
        {
            var entry = set.Entry(trackedEntity);
            entry.CurrentValues.SetValues(entity);
        }
        else
        {
            set.Update(entity);
        }
    }

    public virtual void Delete(TEntity entity)
    {
        var set = DbContext.Set<TEntity>();

        if (GetTrackedEntity(entity) is TEntity trackedEntity)
        {
            set.Entry(trackedEntity).State = EntityState.Deleted;
        }
        else
        {
            set.Remove(entity);
        }
    }

    public async virtual Task SaveAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    private TEntity? GetTrackedEntity(TEntity entity)
    {
        return DbContext.Set<TEntity>().Local.FirstOrDefault(e => e.Id == entity.Id);
    }
}