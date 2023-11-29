using InnowiseClinic.Services.Authorization.Services.Models;

public interface IResolver
{
    Account Resolve(int id);
}