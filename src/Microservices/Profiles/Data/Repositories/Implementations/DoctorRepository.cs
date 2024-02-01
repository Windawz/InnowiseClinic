using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class DoctorRepository : Repository<DoctorEntity>, IDoctorRepository
{
    public DoctorRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Doctors";

    public async Task<ICollection<DoctorEntity>> GetPageFilteredByOfficeAsync(int offset, int count, Guid officeId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return await Entities.AsQueryable()
            .Where(entity => entity.OfficeId == officeId)
            .Skip(offset)
            .Take(count)
            .ToListAsync();
    }

    public async Task<ICollection<DoctorEntity>> GetPageFilteredBySpecializationAsync(int offset, int count, Guid specializationId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return await Entities.AsQueryable()
            .Where(entity => entity.SpecializationId == specializationId)
            .Skip(offset)
            .Take(count)
            .ToListAsync();
    }
}