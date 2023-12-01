using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public interface ILogInAccessor
{
    Account GetAccess(Email email, Password password);
}