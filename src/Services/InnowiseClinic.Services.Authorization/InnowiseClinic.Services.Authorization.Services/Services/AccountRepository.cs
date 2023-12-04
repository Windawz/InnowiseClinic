using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public class AccountRepository : IAccountRepository
{
    private readonly AuthorizationDbContext _dbContext;

    public AccountRepository(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Account> GetAll()
    {
        return _dbContext.Accounts
            .AsNoTracking()
            .AsEnumerable()
            .Select(account => Mapping.MapToServiceLayer(account));
    }

    public bool TryGetById(int id, out Account account)
    {
        var dataAccount = _dbContext.Accounts
            .AsNoTracking()
            .Include(account => account.CreatedBy)
            .Include(account => account.UpdatedBy)
            .SingleOrDefault(account => account.Id == id);

        if (dataAccount is not null)
        {
            account = Mapping.MapToServiceLayer(dataAccount);
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
        var dataAccount = _dbContext.Accounts
            .AsNoTracking()
            .Where(account => account.Email == email.Address)
            .Include(account => account.CreatedBy)
            .Include(account => account.UpdatedBy)
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
            .AsEnumerable()
            .SingleOrDefault(account => new Email(account.Email) == email);

        if (dataAccount is not null)
        {
            account = Mapping.MapToServiceLayer(dataAccount);
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
        var dataAccount = Mapping.MapToDataLayer(account with
        {
            // Just in case, to not mess up the change tracker.
            Id = default,
        });
        _dbContext.Accounts.Add(dataAccount);
    }

    public void Delete(int id)
    {
        var dataAccount = _dbContext.Accounts.Find(id);
        if (dataAccount is not null)
        {
            _dbContext.Remove(dataAccount);
        }
    }

    public void Update(Account account)
    {
        var dataAccount = Mapping.MapToDataLayer(account);
        _dbContext.Accounts.Update(dataAccount);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}