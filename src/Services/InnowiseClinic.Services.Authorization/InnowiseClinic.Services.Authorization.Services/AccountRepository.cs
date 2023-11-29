using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.Services;

public class AccountRepository : IAccountRepository
{
    private readonly AuthorizationDbContext _dbContext;
    private bool _disposed;

    public AccountRepository(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Account> GetAll()
    {
        ThrowIfDisposed();

        return _dbContext.Accounts
            .AsNoTracking()
            .AsEnumerable()
            .Select(account => Mapping.FromDataAccount(account));
    }

    public bool TryGetById(int id, out Account account)
    {
        ThrowIfDisposed();

        var dataAccount = _dbContext.Accounts
            .AsNoTracking()
            .SingleOrDefault(account => account.Id == id);

        if (dataAccount is not null)
        {
            account = Mapping.FromDataAccount(dataAccount);
            return true;
        }
        else
        {
            account = null!;
            return false;
        }
    }

    public bool TryGetByEmail(Email email, out Account account)
    {
        ThrowIfDisposed();

        var dataAccount = _dbContext.Accounts
            .AsNoTracking()
            // Emails may have special comparison rules in business logic
            // (case insensitivity, for example),
            // but it's not guranateed that we will be able to use that logic
            // in a database query.
            //
            // So we do a naive search using default string equality,
            // which will later narrow down.
            //
            // This may fail if the comparison that happens at the database level
            // for some reason rejects some of the entries that the business logic
            // comparison would accept.
            .Where(account => account.Email == email.Address)
            .AsEnumerable()
            .SingleOrDefault(account => new Email(account.Email) == email);

        if (dataAccount is not null)
        {
            account = Mapping.FromDataAccount(dataAccount);
            return true;
        }
        else
        {
            account = null!;
            return false;
        }
    }

    public void Insert(Account account)
    {
        ThrowIfDisposed();

        var dataAccount = Mapping.ToDataAccount(account with
        {
            // Just in case, to not mess up the change tracker.
            Id = default,
        });
        _dbContext.Accounts.Add(dataAccount);
    }

    public void Delete(int id)
    {
        ThrowIfDisposed();

        var dataAccount = _dbContext.Accounts.Find(id);
        if (dataAccount is not null)
        {
            _dbContext.Remove(dataAccount);
        }
    }

    public void Update(Account account)
    {
        ThrowIfDisposed();

        var dataAccount = Mapping.ToDataAccount(account);
        _dbContext.Accounts.Update(dataAccount);
    }

    public void Save()
    {
        ThrowIfDisposed();

        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _dbContext.SaveChanges();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().FullName);
        }
    }
}