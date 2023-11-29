namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Account
{
    public Account(int id, Email email, Password password, Role role)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
    }

    public int Id { get; init; }

    public Email Email { get; init; }

    public Password Password { get; init; }

    public bool IsEmailVerified { get; init; } = false;

    public Account? CreatedBy { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public Account? UpdatedBy { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public Role Role { get; init; }
}