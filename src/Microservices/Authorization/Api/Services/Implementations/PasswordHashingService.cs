using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;

public class PasswordHashingService(IPasswordHasher<string> passwordHasher) : IPasswordHashingService
{
    private readonly IPasswordHasher<string> _passwordHasher = passwordHasher;

    public string GetHashedPassword(string email, string password)
    {
        return _passwordHasher.HashPassword(email, password);
    }
}