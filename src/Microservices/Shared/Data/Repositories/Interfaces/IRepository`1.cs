using InnowiseClinic.Microservices.Shared.Data.Entities;

namespace InnowiseClinic.Microservices.Shared.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}