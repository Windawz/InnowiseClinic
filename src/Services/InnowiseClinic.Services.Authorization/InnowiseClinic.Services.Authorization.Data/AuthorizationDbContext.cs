using System.Reflection;
using InnowiseClinic.Services.Authorization.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.Data;

/// <summary>
/// Provides data access to authorization-related entities.
/// </summary>
public class AuthorizationDbContext : DbContext
{
    /// <summary>
    /// Creates a database context providing data access to authorization-related entities.
    /// </summary>
    /// <param name="options">Database context options.</param>
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

    /// <summary>
    /// Provides access to the user account table.
    /// </summary>
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}