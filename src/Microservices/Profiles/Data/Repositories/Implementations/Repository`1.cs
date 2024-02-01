using System.Linq.Expressions;
using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public abstract class Repository<TEntity> : IRepository<TEntity>
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
        return await Entities.AsQueryable()
            .SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<TEntity?> GetByNamePartsAsync(string firstName, string lastName, string? middleName)
    {
        // Could've tried a static method here instead
        // to compare name parts in a uniform manner,
        // but I'm not sure how the MongoDB driver will
        // translate a static method call, if at all.
        return await Entities.AsQueryable()
            .SingleOrDefaultAsync(entity =>
                entity.FirstName.Equals(firstName, StringComparison.InvariantCulture)
                && entity.LastName.Equals(lastName, StringComparison.InvariantCulture)
                && (middleName == null 
                    && entity.MiddleName == null
                    || middleName != null
                    && entity.MiddleName != null
                    && middleName.Equals(entity.MiddleName, StringComparison.InvariantCultureIgnoreCase)));
    }

    public async Task<ICollection<TEntity>> GetPageAsync(int offset, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return await Entities.AsQueryable()
            .Skip(offset)
            .Take(count)
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