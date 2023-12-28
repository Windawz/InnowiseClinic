using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;

public interface IOfficeRepository : IRepository<OfficeEntity>
{
    Task<ICollection<OfficeEntity>> GetPageAsync(int count, Guid? start = null);
}