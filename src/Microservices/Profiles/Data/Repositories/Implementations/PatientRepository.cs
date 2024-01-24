using InnowiseClinic.Microservices.Profiles.Data.Entities;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class PatientRepository : Repository<PatientEntity>
{
    public PatientRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Patients";
}