using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IProfileRepository<TProfileEntity> where TProfileEntity : ProfileEntity
{
    Task<TProfileEntity?> GetAsync(Guid id);
    Task<Guid> AddAsync(TProfileEntity entity);
    Task UpdateAsync(TProfileEntity entity);
    Task DeleteAsync(TProfileEntity entity);
}