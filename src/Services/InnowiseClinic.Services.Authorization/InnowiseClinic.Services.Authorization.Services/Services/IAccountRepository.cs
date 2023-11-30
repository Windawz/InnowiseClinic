using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public interface IAccountRepository : IDisposable
{
    IEnumerable<Account> GetAll();
    bool TryGetById(int id, out Account account);
    bool TryGetByEmail(Email email, out Account account);
    void Insert(Account account);
    void Delete(int id);
    void Update(Account account);
    void Save();
}