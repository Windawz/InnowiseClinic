using System.Reflection;
using InnowiseClinic.Microservices.Offices.Data.Entities;
using InnowiseClinic.Microservices.Shared.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Offices.Data.Contexts;

public class OfficesDbContext : BasicDbContext<OfficesDbContext>
{
    public OfficesDbContext(DbContextOptions<OfficesDbContext> options) : base(options) { }

    public DbSet<OfficeEntity> Offices =>
        Set<OfficeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}