using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;

public class AccountService(
    IAccountRepository accountRepository,
    IAccountMapperService accountMapperService,
    IPasswordHasher<string> passwordHasher) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IAccountMapperService _accountMapperService = accountMapperService;
    private readonly IPasswordHasher<string> _passwordHasher = passwordHasher;

    public async Task<Account> AccessAccountAsync(string email, string password)
    {
        var account = _accountMapperService.MapFromAccountEntity(
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

        var account = _accountMapperService.MapToAccountEntity(
            new Account(
                Id: Guid.NewGuid(),
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