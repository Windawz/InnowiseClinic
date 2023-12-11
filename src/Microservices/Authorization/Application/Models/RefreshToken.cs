namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record RefreshToken(Guid TokenId, DateTime CreatedAt, DateTime ExpiresAt);