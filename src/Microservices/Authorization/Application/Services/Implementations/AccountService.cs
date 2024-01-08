using InnowiseClinic.Microservices.Authorization.Application.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Mapping;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher<string> _passwordHasher;

    public AccountService(
        IAccountRepository accountRepository,
        IPasswordHasher<string> passwordHasher)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Account> AccessAccountAsync(string email, string password)
    {
        var account = AccountMapping.ToAccount(
            await _accountRepository.GetAsync(email)
                ?? throw new AccountNotFoundException(email));

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(
            user: email,
            hashedPassword: account.Password,
            providedPassword: password);
        
        if (passwordVerificationResult is PasswordVerificationResult.Failed)
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

        password = _passwordHasher.HashPassword(email, password);

        var account = AccountMapping.ToAccountEntity(
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
    }
}