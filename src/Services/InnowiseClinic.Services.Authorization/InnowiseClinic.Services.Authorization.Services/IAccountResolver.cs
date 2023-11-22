using InnowiseClinic.Services.Authorization.Data.Models;

namespace InnowiseClinic.Services.Authorization.Services;

public interface IAccountResolver
{
    Account? ResolveById(int id);
    Account? ResolveByEmail(string email);
}