using System.Linq.Expressions;
using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public abstract partial class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected Repository(IMongoDatabase database)
    {
        Entities = database.GetCollection<TEntity>(TableName);
    }

    protected IMongoCollection<TEntity> Entities { get; }

    protected abstract string TableName { get; }

    public async Task<TEntity?> GetAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);

        return await Entities.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<ICollection<TEntity>> GetPageAsync(int offset, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return await Entities.Aggregate()
            .Skip(offset)
            .Limit(count)
            .ToListAsync();
    }

    public async Task<ICollection<TEntity>> GetPageFilteredAsync<TProperty>(
        int offset,
        int count,
        Expression<Func<TEntity, TProperty>> filterSelector,
        TProperty filterValue)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var filter = Builders<TEntity>.Filter.Eq(filterSelector, filterValue);

        return await Entities.Find(filter)
            .Skip(offset)
            .Limit(count)
            .ToListAsync();
    }

    public async Task<Guid> AddAsync(TEntity entity)
    {
        var newId = Guid.NewGuid();

        await Entities.InsertOneAsync(entity with { Id = newId });

        return newId;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await Entities.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await Entities.DeleteOneAsync(filter);
    }
}