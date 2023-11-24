using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnowiseClinic.Services.Authorization.Data.Models;

/// <summary>
/// Configuration for the <see cref="Role"/> entity.
/// </summary>
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the <see cref="Role"/> entity.
    /// </summary>
    /// <param name="builder"><inheritdoc/></param>
    public void Configure(EntityTypeBuilder<Role> builder) { }
}