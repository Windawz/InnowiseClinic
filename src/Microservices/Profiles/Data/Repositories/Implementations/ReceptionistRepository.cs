using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class ReceptionistRepository : Repository<ReceptionistEntity>
{
    public ReceptionistRepository(
        IDbConnectionFactory connectionFactory,
        IEntityMetadataProvider entityMetadataProvider,
        ISqlValueFormatter sqlValueFormatter) : base(
            connectionFactory,
            entityMetadataProvider,
            sqlValueFormatter) { }

    protected override IEnumerable<(string Key, object? Value)> GetPropertyNamesAndValues(ReceptionistEntity entity)
    {
        return new (string, object?)[]
        {
            (nameof(ReceptionistEntity.AccountId), entity.AccountId),
            (nameof(ReceptionistEntity.OfficeId), entity.OfficeId),
            (nameof(ReceptionistEntity.FirstName), entity.FirstName),
            (nameof(ReceptionistEntity.LastName), entity.LastName),
            (nameof(ReceptionistEntity.MiddleName), entity.MiddleName),
        };
    }
}