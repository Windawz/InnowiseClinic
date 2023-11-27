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

    /// <summary>
    /// Creates an instance of <see cref="AccountRegistrator"/>.
    /// </summary>
    /// <param name="dbContext">An instance of <see cref="AuthorizationDbContext"/>.</param>
    public AccountRegistrator(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Account RegisterOther(Account initiator, Email email, string password, IReadOnlyCollection<Role> roles)
    {
        var account = new Account(default, email, password, roles)
        {
            CreatedBy = initiator,
        };
        _dbContext.Accounts.Add(
            Mapping.ToDataAccount(account));
        _dbContext.SaveChanges();
        return account;
    }

    /// <inheritdoc/>
    public Account RegisterSelf(Email email, string password)
    {
        var account = new Account(default, email, password, Role.Patient);
        _dbContext.Accounts.Add(
            Mapping.ToDataAccount(account));
        _dbContext.SaveChanges();
        return account;
    }
}