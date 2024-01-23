using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class ReceptionistProfileRepository : ProfileRepository<ReceptionistProfileEntity>
{
    public ReceptionistProfileRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "ReceptionistProfiles";
}