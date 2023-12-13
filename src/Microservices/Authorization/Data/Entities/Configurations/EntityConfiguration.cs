using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Microservices.Authorization.Data.Entities.Configurations;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.UseTpcMappingStrategy();
        
        builder.Property(entity => entity.Id)
            .ValueGeneratedNever();
    }
}
