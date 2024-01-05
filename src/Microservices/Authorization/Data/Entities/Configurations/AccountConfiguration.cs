using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Microservices.Authorization.Data.Entities.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasData([
            new()
            {
                Id = Guid.Parse("71992c68-c246-49d5-a22f-84fae24cba89"),
                Email = "admin@mydomain.com",
                Password = "AQAAAAIAAYagAAAAEAsfZPi+5Ij52HHpQMwi5cOWHuShAyGhZ/QG34onfHAjDUuYYZWM94ARK2EM1LRgwQ==",
                IsEmailVerified = true,
                CreatedByEmail = null,
                CreatedAt = new DateTime(year: 2023, month: 1, day: 1),
                UpdatedByEmail = null,
                UpdatedAt = null,
                Role = "receptionist",
            }
        ]);
    }
}