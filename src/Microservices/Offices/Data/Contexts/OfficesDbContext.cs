using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Offices.Data.Contexts;

public class OfficesDbContext : DbContext
{
    public OfficesDbContext(DbContextOptions<OfficesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}