using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class PatientProfileRepository : ProfileRepository<PatientProfileEntity>
{
    public PatientProfileRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "PatientProfiles";
}