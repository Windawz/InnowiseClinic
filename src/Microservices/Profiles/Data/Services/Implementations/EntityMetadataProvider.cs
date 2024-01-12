using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Entities.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Implementations;

public class EntityMetadataProvider : IEntityMetadataProvider
{
    private static readonly IReadOnlyDictionary<Type, string> _tableNames = new Dictionary<Type, string>()
    {
        [typeof(PatientEntity)] = "Patients",
        [typeof(DoctorEntity)] = "Doctors",
        [typeof(ReceptionistEntity)] = "Receptionists",
    };

    /// <exception cref="ArgumentException" />
    public string GetTableName<TEntity>() where TEntity : IEntity
    {
        return _tableNames.GetValueOrDefault(typeof(TEntity))
            ?? throw new ArgumentException(
                message: $"Unknown entity type {typeof(TEntity)}",
                paramName: nameof(TEntity));
    }
}