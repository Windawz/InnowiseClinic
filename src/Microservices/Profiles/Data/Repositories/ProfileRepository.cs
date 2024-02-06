using InnowiseClinic.Microservices.Profiles.Application.Models;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Application.Repositories.Filters;
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

    public async Task<TProfile?> GetAsync<TProfile>(IFilter filter) where TProfile : Profile
    {
        var visitor = new FilterVisitor(_collection.AsQueryable());
        filter.Accept(visitor);
        var document = await visitor.Queryable.SingleOrDefaultAsync();
        
        return document is not null
            ? DataToApplicationMap.ToProfile<TProfile>(document)
            : null;
    }

    public async Task<ICollection<TProfile>> GetManyAsync<TProfile>(
        IFilter? filter,
        int? lastPosition,
        int? maxCount) where TProfile : Profile
    {
        var queryable = _collection.AsQueryable();

        if (filter is not null)
        {
            var visitor = new FilterVisitor(queryable);
            filter.Accept(visitor);
            queryable = visitor.Queryable;
        }

        if (lastPosition is { } position)
        {
            queryable = queryable.Skip(position);
        }

        if (maxCount is { } count)
        {
            queryable = queryable.Take(count);
        }

        return await queryable.Select(document => DataToApplicationMap.ToProfile<TProfile>(document))
            .ToListAsync();
    }

    public Task<RepositoryUpdateResult> UpdateAsync<TProfile>(TProfile profile) where TProfile : Profile
    {
        throw new NotImplementedException();
    }

    private class FilterVisitor : IFilterVisitor
    {
        public FilterVisitor(IMongoQueryable<ProfileDocument> queryable)
        {
            Queryable = queryable;
        }

        public IMongoQueryable<ProfileDocument> Queryable { get; private set; }

        public void VisitNameFilter(NameFilter filter)
        {
            Queryable = Queryable.Where(document =>
                document.FirstName == filter.Name.First
                && document.LastName == filter.Name.Last
                && document.MiddleName == filter.Name.Middle);
        }

        public void VisitOfficeFilter(OfficeFilter filter)
        {
            Queryable = Queryable.Where(document =>
                document.OfficeId.HasValue
                && document.OfficeId.Value == filter.OfficeId);
        }

        public void VisitSpecializationFilter(SpecializationFilter filter)
        {
            Queryable = Queryable.Where(document =>
                document.SpecializationId.HasValue
                && document.SpecializationId.Value == filter.SpecializationId);
        }
    }
}
