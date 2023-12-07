using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

public interface IAsyncRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveAsync();
}