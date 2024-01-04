namespace InnowiseClinic.Microservices.Authorization.Application.Models;

public record Account(
    Guid Id,
    string Email,
    string Password,
    bool IsEmailVerified,
    string? CreatedByEmail,
    DateTime CreatedAt,
    string? UpdatedByEmail,
    DateTime? UpdatedAt,
    Role Role);