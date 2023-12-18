using InnowiseClinic.Microservices.Offices.Data.Contexts;
using InnowiseClinic.Microservices.Shared.Data.DesignTimeDbContextFactories;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Offices.Data.DesignTimeDbContextFactories;

public class OfficesDbContextDesignTimeFactory : DesignTimeDbContextFactory<OfficesDbContext>
{
    protected override string? DefaultJsonConfigurationFileName => Path.Combine(
        "..", "Api", "appsettings.Development.json");

    protected override void ConfigureDbContextOptions(DbContextOptionsBuilder<OfficesDbContext> optionsBuilder, string connectionString)
    {
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override OfficesDbContext InstantiateDbContext(DbContextOptions<OfficesDbContext> options)
    {
        return new OfficesDbContext(options);
    }
}