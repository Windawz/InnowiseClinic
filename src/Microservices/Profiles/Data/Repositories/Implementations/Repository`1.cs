using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;

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

    // There was supposed to be a method allowing to query
    // a set of profiles filtered by multiple predicate expression
    // trees.
    // There are, however, more urgent things to do,
    // and right now there is no need for it, so better
    // leave that for another time.

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