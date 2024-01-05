using InnowiseClinic.Microservices.Authorization.Application.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Models;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;

public interface IAccountService
{
    /// <exception cref="AccountNotFoundException"/>
    /// /// <exception cref="InvalidPasswordException"/>
    Task<Account> AccessAccountAsync(string email, string password);

    /// <exception cref="AccountAlreadyExistsException"/>
    Task CreateAccountAsync(string email, string password, Role role);
}