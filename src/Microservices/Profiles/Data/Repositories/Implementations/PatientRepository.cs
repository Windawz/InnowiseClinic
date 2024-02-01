using InnowiseClinic.Microservices.Profiles.Data.Entities;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;

public class PatientRepository : Repository<PatientEntity>, IPatientRepository
{
    public PatientRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase) { }

    protected override string TableName => "Patients";
}