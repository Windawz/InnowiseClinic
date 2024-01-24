using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class DoctorRepository : Repository<DoctorEntity>
{
    public DoctorRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Doctors";
}