using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class DoctorRepository : Repository<DoctorEntity>
{
    public DoctorRepository(
        IDbConnectionFactory connectionFactory,
        ISqlValueFormatter sqlValueFormatter) : base(
            connectionFactory,
            sqlValueFormatter) { }

    protected override string TableName => "Doctors";

    protected override IEnumerable<(string Key, object? Value)> GetPropertyNamesAndValues(DoctorEntity entity)
    {
        return new (string, object?)[]
        {
            (nameof(DoctorEntity.AccountId), entity.AccountId),
            (nameof(DoctorEntity.OfficeId), entity.OfficeId),
            (nameof(DoctorEntity.SpecializationId), entity.SpecializationId),
            (nameof(DoctorEntity.FirstName), entity.FirstName),
            (nameof(DoctorEntity.LastName), entity.LastName),
            (nameof(DoctorEntity.MiddleName), entity.MiddleName),
            (nameof(DoctorEntity.DateOfBirth), entity.DateOfBirth),
            (nameof(DoctorEntity.CareerStartYear), entity.CareerStartYear),
            (nameof(DoctorEntity.Status), entity.Status),
        };
    }
}