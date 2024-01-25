using System.Linq.Expressions;
using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAsync(Guid id);
    Task<ICollection<TEntity>> GetPageAsync(int offset, int count);
    Task<ICollection<TEntity>> GetPageFilteredAsync<TProperty>(
        int offset,
        int count,
        Expression<Func<TEntity, TProperty>> filterSelector,
        TProperty filterValue);
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}