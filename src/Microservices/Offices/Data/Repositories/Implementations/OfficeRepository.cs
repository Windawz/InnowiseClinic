using InnowiseClinic.Microservices.Offices.Data.Contexts;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Views;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Offices.Data.Repositories.Implementations;

public class OfficeRepository : Repository<OfficeEntity, OfficesDbContext>, IOfficeRepository
{
    public OfficeRepository(OfficesDbContext dbContext) : base(dbContext) { }

    public async Task<ICollection<OfficePageEntryView>> GetPageAsync(int count, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));
        ArgumentOutOfRangeException.ThrowIfNegative(offset, nameof(offset));

        return await DbContext.Offices
            .OrderBy(office => office.Id)
            .Skip(offset)
            .Select(office => new
            {
                OfficeId = office.Id,
                office.OfficeNumber,
                office.RegistryPhoneNumber,
                office.IsActive,
            })
            .Take(count)
            .AsAsyncEnumerable()
            .Select(selection => new OfficePageEntryView()
            {
                OfficeId = selection.OfficeId,
                OfficeNumber = selection.OfficeNumber,
                RegistryPhoneNumber = selection.RegistryPhoneNumber,
                IsActive = selection.IsActive,
            })
            .ToListAsync();
    }
}