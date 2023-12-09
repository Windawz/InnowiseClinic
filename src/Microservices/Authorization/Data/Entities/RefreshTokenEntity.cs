namespace InnowiseClinic.Microservices.Authorization.Data.Entities;

public class RefreshTokenEntity : Entity
{
    public required DateTime CreatedAt { get; set; }
    public required DateTime ExpiresAt { get; set; }
}