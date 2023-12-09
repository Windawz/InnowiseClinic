namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record RefreshToken(
    DateTime CreatedAt,
    DateTime ExpiresAt,
    Guid TokenId) : Token(CreatedAt, ExpiresAt);