using System.Data;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}