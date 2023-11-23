using System.Collections.Immutable;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// A default implementation of <see cref="IAccountRegistrator"/>.
/// </summary>
public class AccountRegistrator : IAccountRegistrator
{
    private readonly AuthorizationDbContext _dbContext;
    private readonly IRoleResolver _roleResolver;

    /// <summary>
    /// Creates an instance of <see cref="AccountRegistrator"/>.
    /// </summary>
    /// <param name="dbContext">An instance of <see cref="AuthorizationDbContext"/>.</param>
    /// <param name="roleResolver">An instance of <see cref="IRoleResolver"/>.</param>
    public AccountRegistrator(AuthorizationDbContext dbContext, IRoleResolver roleResolver)
    {
        _dbContext = dbContext;
        _roleResolver = roleResolver;
    }

    /// <inheritdoc/>
    public Account RegisterOther(Account initiator, string email, string password, IReadOnlyCollection<Role> roles)
    {
        var account = CreateAccountFromRegistrationInfo(
            email: email,
            password: password,
            initiator: initiator,
            currentTime: DateTime.UtcNow,
            roles: roles);
        _dbContext.Accounts.Add(
            Mapping.ToDataAccount(account));
        _dbContext.SaveChanges();
        return account;
    }

    /// <inheritdoc/>
    public Account RegisterSelf(string email, string password)
    {
        var account = CreateAccountFromRegistrationInfo(
            email: email,
            password: password,
            initiator: null,
            currentTime: DateTime.UtcNow,
            roles: new[]
            {
                _roleResolver.ResolveByName("patient")
                    ?? throw new InvalidOperationException("Role \"patient\" does not exist")
            });
        _dbContext.Accounts.Add(
            Mapping.ToDataAccount(account));
        _dbContext.SaveChanges();
        return account;
    }

    private static Account CreateAccountFromRegistrationInfo(
        string email,
        string password,
        Account? initiator,
        DateTime currentTime,
        IReadOnlyCollection<Role> roles)
    {
        return new Account(
            Id: default,
            Email: email,
            Password: password,
            PhoneNumber: null,
            IsEmailVerified: false,
            PhotoId: null,
            CreatedBy: initiator,
            CreatedAt: currentTime,
            UpdatedBy: null,
            UpdatedAt: null,
            Roles: roles.ToImmutableHashSet());
    }
}