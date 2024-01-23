using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class DoctorProfileRepository : ProfileRepository<DoctorProfileEntity>
{
    public DoctorProfileRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "DoctorProfiles";
}