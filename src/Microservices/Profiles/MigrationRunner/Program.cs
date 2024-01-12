using FluentMigrator.Runner;
using InnowiseClinic.Microservices.Profiles.Data.Migrations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnowiseClinic.Microservices.Profiles.MigrationRunner;

public class Program
{
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        if (configuration.GetSection("File").Exists())
        {
            string filePath = configuration.GetRequiredSection("File").Value!;
            if (Path.GetExtension(filePath) == ".json" && File.Exists(filePath))
            {
                configuration = new ConfigurationBuilder()
                    .AddConfiguration(configuration)
                    .AddJsonFile(filePath)
                    .Build();
            }
        }

        using var scope = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default")
                    ?? configuration.GetRequiredSection("ConnectionString").Value!)
                .ScanIn(typeof(Initial).Assembly).For.Migrations())
            .AddLogging(builder => builder
                .AddFluentMigratorConsole())
            .AddSingleton<IEntityMetadataProvider, EntityMetadataProvider>()
            .BuildServiceProvider(validateScopes: false)
            .CreateScope();

        scope.ServiceProvider.GetRequiredService<IMigrationRunner>()
            .MigrateUp();
    }
}
