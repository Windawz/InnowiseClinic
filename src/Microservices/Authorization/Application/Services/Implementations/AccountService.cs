using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class AccountService(
    IAccountRepository accountRepository,
    IAccountMapperService accountMapperService) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IAccountMapperService _accountMapperService = accountMapperService;

    public async Task<Account> AccessAccountAsync(string email, string password)
    {
        var account = _accountMapperService.MapFromAccountEntity(
            await _accountRepository.GetAsync(email)
                ?? throw new AccountNotFoundException(email));
        
        if (!account.Password.Equals(password, StringComparison.Ordinal))
        {
            throw new InvalidPasswordException(email, password);
        }

        return account;
    }

    public async Task CreateAccountAsync(string email, string password, Role role)
    {
        var now = DateTime.UtcNow;

        if (await _accountRepository.GetAsync(email) is not null)
        {
            throw new AccountAlreadyExistsException(email);
        }

        var account = _accountMapperService.MapToAccountEntity(
            new Account(
                Id: default,
                Email: email,
                Password: password,
                IsEmailVerified: false,
                CreatedByEmail: null,
                CreatedAt: now,
                UpdatedByEmail: null,
                UpdatedAt: null,
                Role: role));

        await _accountRepository.AddAsync(account);
        await _accountRepository.SaveAsync();
    }
}