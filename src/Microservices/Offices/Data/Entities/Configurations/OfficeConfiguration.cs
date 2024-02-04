using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Microservices.Offices.Data.Entities.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<OfficeEntity>
{
    public void Configure(EntityTypeBuilder<OfficeEntity> builder)
    {
        builder.Property(office => office.City)
            .HasMaxLength(128);

        builder.Property(office => office.Street)
            .HasMaxLength(256);

        builder.Property(office => office.HouseNumber)
            .HasMaxLength(64);

        builder.Property(office => office.OfficeNumber)
            .HasMaxLength(64);

        builder.Property(office => office.RegistryPhoneNumber)
            .HasMaxLength(64);
    }
}
