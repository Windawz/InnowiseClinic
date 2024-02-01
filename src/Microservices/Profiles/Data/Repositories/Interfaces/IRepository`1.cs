using System.Linq.Expressions;
using InnowiseClinic.Microservices.Profiles.Data.Entities;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetAsync(Guid id);
    Task<TEntity?> GetByNamePartsAsync(string firstName, string lastName, string? middleName);
    Task<ICollection<TEntity>> GetPageAsync(int offset, int count);
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}