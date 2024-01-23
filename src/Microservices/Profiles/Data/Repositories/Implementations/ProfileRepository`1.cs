using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public abstract partial class ProfileRepository<TProfileEntity> : IProfileRepository<TProfileEntity>
    where TProfileEntity : ProfileEntity
{
    protected ProfileRepository(IMongoDatabase database)
    {
        Entities = database.GetCollection<TProfileEntity>(TableName);
    }

    protected IMongoCollection<TProfileEntity> Entities { get; }

    protected abstract string TableName { get; }

    public async Task<TProfileEntity?> GetAsync(Guid id)
    {
        var filter = Builders<TProfileEntity>.Filter.Eq(entity => entity.Id, id);

        return await Entities.Find(filter).FirstOrDefaultAsync();
    }

    // There was supposed to be a method allowing to query
    // a set of profiles filtered by multiple predicate expression
    // trees.
    // There are, however, more urgent things to do,
    // and right now there is no need for it, so better
    // leave that for another time.

    public async Task<Guid> AddAsync(TProfileEntity entity)
    {
        var newId = Guid.NewGuid();

        await Entities.InsertOneAsync(entity with { Id = newId });

        return newId;
    }

    public async Task UpdateAsync(TProfileEntity entity)
    {
        var filter = Builders<TProfileEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await Entities.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(TProfileEntity entity)
    {
        var filter = Builders<TProfileEntity>.Filter.Eq(entity => entity.Id, entity.Id);

        await Entities.DeleteOneAsync(filter);
    }
}