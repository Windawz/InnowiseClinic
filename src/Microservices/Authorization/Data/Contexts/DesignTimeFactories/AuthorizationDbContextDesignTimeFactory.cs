using InnowiseClinic.Microservices.Authorization.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InnowiseClinic.Microservices.Authorization.Data.Contexts.DesignTimeFactories;

public class AuthorizationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthorizationDbContext>
{
    public const string ConfigurationFileNameConfigurationKey = "File";
    public const string ConnectionStringConfigurationKey = "ConnectionStrings:Default";
    public static readonly string DefaultConfigurationFileName = Path.GetFullPath(
        Path.Combine("..", "Api", "appsettings.Development.json"));

    public AuthorizationDbContext CreateDbContext(string[] args)
    {
        var configuration = MakeConfiguration(args);

        string connectionString = configuration[ConnectionStringConfigurationKey]
            ?? throw new DesignTimeFactoryException(
                $"No value for configuration key \"{ConnectionStringConfigurationKey}\" found in configuration."
                + " You can pass it through the command line or by providing a configuration .json file name"
                + $" through the \"{ConfigurationFileNameConfigurationKey}\" configuration key"
                + $" (set to \"{DefaultConfigurationFileName}\" by default)");

        var optionsBuilder = new DbContextOptionsBuilder<AuthorizationDbContext>()
            .UseSqlServer(connectionString);
        
        return new AuthorizationDbContext(optionsBuilder.Options);
    }

    private static IConfigurationRoot MakeConfiguration(string[] args)
    {
        var mainConfigurationBuilder = new ConfigurationBuilder();

        var commandLineConfiguration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        mainConfigurationBuilder.AddConfiguration(commandLineConfiguration);

        string configurationFileName = commandLineConfiguration[ConfigurationFileNameConfigurationKey]
            ?? DefaultConfigurationFileName;

        var jsonConfiguration = new ConfigurationBuilder()
            .AddJsonFile(configurationFileName, optional: true)
            .Build();

        mainConfigurationBuilder.AddConfiguration(jsonConfiguration);

        return mainConfigurationBuilder.Build();
    }
}