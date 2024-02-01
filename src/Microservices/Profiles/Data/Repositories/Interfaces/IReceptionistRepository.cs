using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IReceptionistRepository : IRepository<ReceptionistEntity>
{
    Task<ICollection<ReceptionistEntity>> GetPageFilteredByOfficeAsync(int offset, int count, Guid officeId);
}