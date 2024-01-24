using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAsync(Guid id);
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}