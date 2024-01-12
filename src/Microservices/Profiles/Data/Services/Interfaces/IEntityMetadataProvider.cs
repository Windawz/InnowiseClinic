using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

public interface IEntityMetadataProvider 
{
    /// <exception cref="ArgumentException" />
    string GetTableName<TEntity>() where TEntity : IEntity;
}