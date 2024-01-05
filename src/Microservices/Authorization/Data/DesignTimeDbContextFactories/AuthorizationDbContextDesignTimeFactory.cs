using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Shared.Data.DesignTimeDbContextFactories;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Microservices.Authorization.Data.DesignTimeDbContextFactories;

public class AuthorizationDbContextDesignTimeFactory : DesignTimeDbContextFactory<AuthorizationDbContext>
{
    protected override string DefaultJsonConfigurationFileName => Path.GetFullPath(
        Path.Combine("..", "Api", "appsettings.Development.json"));

    protected override void ConfigureDbContextOptions(DbContextOptionsBuilder<AuthorizationDbContext> optionsBuilder, string connectionString)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override AuthorizationDbContext InstantiateDbContext(DbContextOptions<AuthorizationDbContext> options)
    {
        return new AuthorizationDbContext(options);
    }
}