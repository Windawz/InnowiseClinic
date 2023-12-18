using InnowiseClinic.Microservices.Shared.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Entities;

public class AccountEntity : Entity
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public bool IsEmailVerified { get; set; }

    public string? CreatedByEmail { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? UpdatedByEmail { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public required string Role { get; set; }
}