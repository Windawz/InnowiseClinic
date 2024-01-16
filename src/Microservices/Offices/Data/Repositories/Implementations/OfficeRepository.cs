using InnowiseClinic.Microservices.Offices.Data.Contexts;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Offices.Data.Repositories.Implementations;

public class OfficeRepository : Repository<OfficeEntity, OfficesDbContext>, IOfficeRepository
{
    public OfficeRepository(OfficesDbContext dbContext) : base(dbContext) { }

    public async Task<ICollection<OfficeEntity>> GetPageAsync(int count, Guid? start = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));

        IQueryable<OfficeEntity> queryable = DbContext.Offices
            .OrderBy(office => office.Id);

        if (start is Guid guid)
        {
            queryable = queryable
                .Where(office => office.Id.CompareTo(guid) > 0);
        }

        return await queryable
            .Take(count)
            .ToListAsync();
    }
}