using System.Reflection;
using InnowiseClinic.Microservices.Authorization.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.Contexts;

public class AuthorizationDbContext : BasicDbContext<AuthorizationDbContext>
{
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options) { }

    public DbSet<AccountEntity> Accounts =>
        Set<AccountEntity>();

    public DbSet<RefreshTokenEntity> RefreshTokens =>
        Set<RefreshTokenEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}