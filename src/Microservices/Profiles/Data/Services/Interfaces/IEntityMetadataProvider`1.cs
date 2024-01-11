using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

public interface IEntityMetadataProvider<TEntity> where TEntity : IEntity
{
    string TableName { get; }
}