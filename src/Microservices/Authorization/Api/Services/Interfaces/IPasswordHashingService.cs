namespace InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;

public interface IPasswordHashingService
{
    string GetHashedPassword(string email, string password);
}