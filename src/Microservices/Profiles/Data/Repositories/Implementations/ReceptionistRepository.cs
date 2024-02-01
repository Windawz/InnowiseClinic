using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class ReceptionistRepository : Repository<ReceptionistEntity>, IReceptionistRepository
{
    public ReceptionistRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Receptionists";

    public async Task<ICollection<ReceptionistEntity>> GetPageFilteredByOfficeAsync(int offset, int count, Guid officeId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return await Entities.AsQueryable()
            .Where(entity => entity.OfficeId == officeId)
            .Skip(offset)
            .Take(count)
            .ToListAsync();
    }
}