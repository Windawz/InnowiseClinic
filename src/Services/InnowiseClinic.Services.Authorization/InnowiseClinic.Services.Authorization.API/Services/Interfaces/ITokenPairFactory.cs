using InnowiseClinic.Services.Authorization.API.Models;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Services.Interfaces;

public interface ITokenPairFactory
{
    TokenPair Create(Account account);
}