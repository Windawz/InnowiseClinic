using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Services.Authorization.Data.Models;

/// <summary>
/// Configuration for the <see cref="Account"/> entity.
/// </summary>
internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    /// <summary>
    /// Configures the <see cref="Account"/> entity.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Set up navigation properties for entries in the same table.
        builder.HasOne(account => account.CreatedBy)
            .WithOne();

        builder.HasOne(account => account.UpdatedBy)
            .WithOne();
    }
}