using InnowiseClinic.Services.Authorization.Data.Models;

namespace InnowiseClinic.Services.Authorization.Services;

public interface IRoleFinder
{
    Role? ResolveByName(string name);
}