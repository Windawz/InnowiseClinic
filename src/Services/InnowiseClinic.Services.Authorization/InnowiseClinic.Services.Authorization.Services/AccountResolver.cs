using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// A default implementation of <see cref="IAccountResolver"/>.
/// </summary>
public class AccountResolver : IAccountResolver
{
    private readonly AuthorizationDbContext _dbContext;

    /// <summary>
    /// Creates an instance of <see cref="AccountResolver"/>.
    /// </summary>
    /// <param name="dbContext">An instance of <see cref="AuthorizationDbContext"/>.</param>
    public AccountResolver(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Account? ResolveByEmail(Email email)
    {
        var dataAccount = _dbContext.Accounts
            .FirstOrDefault(account => account.Email == email.Address);

        if (dataAccount is not null)
        {
            return Mapping.FromDataAccount(dataAccount);
        }
        else
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public Account? ResolveById(int id)
    {
        var dataAccount = _dbContext.Accounts
            .SingleOrDefault(account => account.Id == id);

        if (dataAccount is not null)
        {
            return Mapping.FromDataAccount(dataAccount);
        }
        else
        {
            return null;
        }
    }
}