using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services.Services;

public class LogInAccessor : ILogInAccessor
{
    private readonly IAccountRepository _repository;

    public LogInAccessor(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Account GetAccess(Email email, Password password)
    {
        if (_repository.TryGetByEmail(email, out var account))
        {
            if (account.Password == password)
            {
                return account;
            }
            else
            {
                throw new PasswordDoesNotMatchException(password.Text);
            }
        }
        else
        {
            throw new AccountWithGivenEmailDoesNotExistException(email.Address);
        }
    }
}