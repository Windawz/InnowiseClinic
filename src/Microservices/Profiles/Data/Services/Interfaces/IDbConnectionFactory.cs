using System.Data.Common;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection();
}