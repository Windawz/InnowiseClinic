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
    public async Task<Account> AccessAccountAsync(string email, string password)
    {
        var account = accountMapperService.MapFromAccountEntity(
            await accountRepository.GetAsync(email)
                ?? throw new AccountNotFoundException(email));

        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(
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

        if (await accountRepository.GetAsync(email) is not null)
        {
            throw new AccountAlreadyExistsException(email);
        }

        password = passwordHasher.HashPassword(email, password);

        var account = accountMapperService.MapToAccountEntity(
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

        await accountRepository.AddAsync(account);
        await accountRepository.SaveAsync();
    }
}