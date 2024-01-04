namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record RefreshToken(Guid TokenId, Role Role, DateTime CreatedAt, DateTime ExpiresAt);