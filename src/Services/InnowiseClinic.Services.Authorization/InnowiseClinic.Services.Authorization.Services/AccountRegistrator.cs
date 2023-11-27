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
    private readonly IAccountResolver _resolver;

    /// <summary>
    /// Creates an instance of <see cref="AccountRegistrator"/>.
    /// </summary>
    /// <param name="dbContext">An instance of <see cref="AuthorizationDbContext"/>.</param>
    public AccountRegistrator(AuthorizationDbContext dbContext, IAccountResolver resolver)
    {
        _dbContext = dbContext;
        _resolver = resolver;
    }

    /// <inheritdoc/>
    public Account RegisterOther(Account initiator, Email email, string password, IReadOnlyCollection<Role> roles)
    {
        ThrowIfNotPermittedToAssignRoles(initiator, email, roles);
        ThrowIfEmailAlreadyOccupied(email);
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
        ThrowIfEmailAlreadyOccupied(email);
        var account = new Account(default, email, password, Role.Patient);
        _dbContext.Accounts.Add(
            Mapping.ToDataAccount(account));
        _dbContext.SaveChanges();
        return account;
    }

    private void ThrowIfEmailAlreadyOccupied(Email email)
    {
        if (_resolver.ResolveByEmail(email) is not null)
        {
            throw new AccountAlreadyExistsException(email);
        }
    }

    private static void ThrowIfNotPermittedToAssignRoles(Account initiator, Email accountEmail, IReadOnlyCollection<Role> roles)
    {
        var registerableRoles = initiator.Roles
            .SelectMany(role => role.RegisterableRoles)
            .Distinct();

        if (roles.Any(role => !registerableRoles.Contains(role)))
        {
            throw new NotPermittedToAssignRoleException(initiator, accountEmail, roles);
        }
    }
}