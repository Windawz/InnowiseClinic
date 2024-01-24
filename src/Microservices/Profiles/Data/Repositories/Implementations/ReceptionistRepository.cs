using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class ReceptionistRepository : Repository<ReceptionistEntity>
{
    public ReceptionistRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Receptionists";
}