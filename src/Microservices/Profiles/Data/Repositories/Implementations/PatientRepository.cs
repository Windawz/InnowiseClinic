using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class PatientRepository : Repository<PatientEntity>
{
    public PatientRepository(
        IDbConnectionFactory connectionFactory,
        IEntityMetadataProvider entityMetadataProvider,
        ISqlValueFormatter sqlValueFormatter) : base(
            connectionFactory,
            entityMetadataProvider,
            sqlValueFormatter) { }

    protected override IEnumerable<(string Key, object? Value)> GetPropertyNamesAndValues(PatientEntity entity)
    {
        return new (string, object?)[]
        {
            (nameof(PatientEntity.AccountId), entity.AccountId),
            (nameof(PatientEntity.FirstName), entity.FirstName),
            (nameof(PatientEntity.LastName), entity.LastName),
            (nameof(PatientEntity.MiddleName), entity.MiddleName),
            (nameof(PatientEntity.PhoneNumber), entity.PhoneNumber),
            (nameof(PatientEntity.DateOfBirth), entity.DateOfBirth),
        };
    }
}