using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.Services;

public class Resolver : IResolver
{
    private readonly IAccountRepository _repository;

    public Resolver(IAccountRepository repository)
    {
        _repository = repository;
    }

    public Account Resolve(int id)
    {
        if (_repository.TryGetById(id, out Account account))
        {
            return account;
        }
        else
        {
            throw new FailedToResolveAccountException(id);
        }
    }
}