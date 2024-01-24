namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record RefreshToken(Guid TokenId, Guid AccountId, Role Role, DateTime CreatedAt, DateTime ExpiresAt);