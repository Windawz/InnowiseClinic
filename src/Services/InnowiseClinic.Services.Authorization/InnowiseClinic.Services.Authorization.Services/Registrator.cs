using System.Collections.Immutable;
using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// A default implementation of <see cref="IRegistrator"/>.
/// </summary>
public class Registrator : IRegistrator
{
    private readonly IAccountRepository _repository;

    public Registrator(IAccountRepository repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public Account RegisterOther(Account initiator, Email email, Password password, Role role)
    {
        ThrowIfEmailAlreadyOccupied(email);

        var account = new Account(default, email, password, role)
        {
            CreatedBy = initiator,
        };
        _repository.Insert(account);
        _repository.Save();
        return account;
    }

    /// <inheritdoc/>
    public Account RegisterSelf(Email email, Password password)
    {
        ThrowIfEmailAlreadyOccupied(email);

        var account = new Account(default, email, password, new Role(RoleNames.Patient));
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
}