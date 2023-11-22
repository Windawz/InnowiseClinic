using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

public interface IAccountRegistrator
{
    Account RegisterSelf(RegistrationInfo info);
    Account RegisterOther(RegistrationInfo info, Account initiator);
}