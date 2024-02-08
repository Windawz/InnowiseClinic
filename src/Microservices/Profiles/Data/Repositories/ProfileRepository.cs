using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Data.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly IMongoCollection<ProfileDocument> _collection;

    public ProfileRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ProfileDocument>(ProfileDocument.CollectionName);
    }

    public async Task<RepositoryAddResult> AddAsync<TProfile>(TProfile profile) where TProfile : Profile
    {
        if (!profile.IsTransient)
        {
            return RepositoryAddResult.AlreadyExists;
        }

        Entity.SetId(profile, Guid.NewGuid());

        var document = DataToApplicationMap.ToDocument(profile);

        await _collection.InsertOneAsync(document);

        return RepositoryAddResult.Added;
    }

    public async Task<RepositoryDeleteResult> DeleteAsync(Guid id)
    {
        var filter = Builders<ProfileDocument>.Filter.Eq(document => document.Id, id);
        var result = await _collection.DeleteOneAsync(filter);

        return result switch
        {
            { DeletedCount: > 0 } => RepositoryDeleteResult.Deleted,
            _ => RepositoryDeleteResult.DoesNotExist,
        };
    }

    public async Task<TProfile?> GetAsync<TProfile>(Guid id) where TProfile : Profile
    {
        var document = await _collection.AsQueryable()
            .SingleOrDefaultAsync(document => document.Id == id);

        return document is not null
            ? DataToApplicationMap.ToProfile<TProfile>(document)
            : null;
    }

    public async Task<ICollection<TProfile>> GetManyAsync<TProfile>(
        Filter? filter,
        int? lastPosition,
        int? maxCount) where TProfile : Profile
    {
        var queryable = _collection.AsQueryable();
        
        if (filter is not null)
        {
            ApplyFilter(queryable, filter);
        }

        if (lastPosition is int position)
        {
            queryable = queryable.Skip(position);
        }

        if (maxCount is int count)
        {
            queryable = queryable.Take(count);
        }

        return (await queryable.ToListAsync())
            .Select(document => DataToApplicationMap.ToProfile<TProfile>(document))
            .ToList();
    }

    public async Task<RepositoryUpdateResult> UpdateAsync<TProfile>(TProfile profile) where TProfile : Profile
    {
        if (profile.IsTransient)
        {
            return RepositoryUpdateResult.DoesNotExist;
        }

        var replacement = DataToApplicationMap.ToDocument(profile);
        var filter = Builders<ProfileDocument>.Filter.Eq(document => document.Id, replacement.Id);

        await _collection.ReplaceOneAsync(filter, replacement);

        return RepositoryUpdateResult.Updated;
    }

    private static IMongoQueryable<ProfileDocument> ApplyFilter(IMongoQueryable<ProfileDocument> queryable, Filter filter)
    {
        if (filter.Name is Name name)
        {
            queryable = queryable.Where(document =>
                document.FirstName == name.First
                && document.LastName == name.Last
                && document.MiddleName == name.Middle);
        }

        if (filter.OfficeId is Guid officeId)
        {
            queryable = queryable.Where(document =>
                document.OfficeId == officeId);
        }

        if (filter.SpecializationId is Guid specializationId)
        {
            queryable = queryable.Where(document =>
                document.SpecializationId == specializationId);
        }

        return queryable;
    }
}
