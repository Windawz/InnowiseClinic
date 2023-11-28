using System.Collections.Immutable;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// A default implementation of <see cref="IAccountRegistrator"/>.
/// </summary>
public class AccountRegistrator : IAccountRegistrator
{
    private readonly IAccountRepository _repository;

    public AccountRegistrator(IAccountRepository repository)
    {
        _repository = repository;
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
        _repository.Insert(account);
        _repository.Save();
        return account;
    }

    /// <inheritdoc/>
    public Account RegisterSelf(Email email, string password)
    {
        ThrowIfEmailAlreadyOccupied(email);

        var account = new Account(default, email, password, Role.Patient);
        _repository.Insert(account);
        return account;
    }

    private void ThrowIfEmailAlreadyOccupied(Email email)
    {
        if (_repository.TryGetByEmail(email, out _))
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