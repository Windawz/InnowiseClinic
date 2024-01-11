using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Implementations;

public class ReceptionistEntityMetadataProvider : IEntityMetadataProvider<ReceptionistEntity>
{
    public string TableName => "Receptionists";
}