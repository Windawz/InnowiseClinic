using InnowiseClinic.Services.Authorization.API.Auth.Models;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Auth.Services.Interfaces;

public interface ITokenPairFactory
{
    TokenPair Create(Account account);
}