using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;

public interface IAccountMapperService
{
    Account MapFromAccountEntity(AccountEntity entity);
    AccountEntity MapToAccountEntity(Account account);
}