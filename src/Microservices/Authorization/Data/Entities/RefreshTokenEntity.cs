using InnowiseClinic.Microservices.Shared.Data.Entities;

namespace InnowiseClinic.Microservices.Authorization.Data.Entities;

public class RefreshTokenEntity : Entity
{
    public required Guid AccountId { get; set; }
    public AccountEntity Account { get; set; } = null!; // Automatically set by EF Core.
                                                        // See Microsoft code at https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many#required-one-to-many
    public required string Role { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}