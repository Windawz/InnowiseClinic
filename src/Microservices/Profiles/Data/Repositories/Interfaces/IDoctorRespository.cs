using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IDoctorRepository : IRepository<DoctorEntity>
{
    Task<ICollection<DoctorEntity>> GetPageFilteredByOfficeAsync(int offset, int count, Guid officeId);
    Task<ICollection<DoctorEntity>> GetPageFilteredBySpecializationAsync(int offset, int count, Guid specializationId);
}