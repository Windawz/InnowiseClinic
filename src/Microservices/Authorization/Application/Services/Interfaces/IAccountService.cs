using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IAccountService
{
    /// <exception cref="Exceptions.AccountNotFoundException">
    Task<Account> AccessAccountAsync(string email, string password);

    /// <exception cref="Exceptions.AccountAlreadyExistsException"/>
    Task CreateAccountAsync(string email, string password, string roleName);
}