using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}