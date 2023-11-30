using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public interface IRegistrator
{
    Account RegisterSelf(Email email, Password password);
    Account RegisterOther(Account initiator, Email email, Password password, Role role);
}