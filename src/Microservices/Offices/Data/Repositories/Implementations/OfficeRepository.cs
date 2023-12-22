using InnowiseClinic.Microservices.Offices.Data.Contexts;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Implementations;

namespace InnowiseClinic.Microservices.Offices.Data.Repositories.Implementations;

public class OfficeRepository : AsyncRepository<OfficeEntity, OfficesDbContext>, IOfficeRepository
{
    public OfficeRepository(OfficesDbContext dbContext) : base(dbContext) { }
}