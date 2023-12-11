namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record AccessToken(
    string SignedValue,
    DateTime CreatedAt,
    DateTime ExpiresAt,
    string TokenType);