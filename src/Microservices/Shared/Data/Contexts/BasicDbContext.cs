using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Shared.Data.Contexts;

public abstract class BasicDbContext<TContext> : DbContext where TContext : DbContext
{
    protected BasicDbContext(DbContextOptions<TContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}