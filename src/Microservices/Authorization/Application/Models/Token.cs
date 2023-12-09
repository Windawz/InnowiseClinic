namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public abstract record Token(DateTime CreatedAt, DateTime ExpiresAt);