using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Microservices.Authorization.Data.Entities.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        var hasher = new PasswordHasher<string>();
        string email = "admin@mydomain.com";
        string password = hasher.HashPassword(email, "12345678");

        builder.HasData([
            new()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = password,
                IsEmailVerified = true,
                Role = "receptionist",
            }
        ]);
    }
}