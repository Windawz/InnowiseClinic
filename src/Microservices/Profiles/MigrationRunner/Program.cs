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
        IConfiguration commandLineConfiguration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        IConfiguration configuration = commandLineConfiguration;

        if (commandLineConfiguration.GetSection("File").Value is string filePath)
        {
            if (Path.GetExtension(filePath) == ".json" && File.Exists(filePath))
            {
                configuration = new ConfigurationBuilder()
                    .AddConfiguration(commandLineConfiguration)
                    .AddJsonFile(filePath)
                    .Build();
            }
        }

        using var scope = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default")
                    ?? commandLineConfiguration.GetRequiredSection("ConnectionString").Value!)
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
