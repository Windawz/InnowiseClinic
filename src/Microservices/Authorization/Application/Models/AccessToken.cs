namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record AccessToken(
    DateTime CreatedAt,
    DateTime ExpiresAt,
    string TokenType,
    Role Role) : Token(CreatedAt, ExpiresAt);