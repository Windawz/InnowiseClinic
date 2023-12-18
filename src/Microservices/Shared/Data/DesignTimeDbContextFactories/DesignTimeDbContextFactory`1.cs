using InnowiseClinic.Microservices.Shared.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InnowiseClinic.Microservices.Shared.Data.DesignTimeDbContextFactories;

public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    private const string JsonConfigurationFileNameConfigurationKey = "File";
    private const string ConnectionStringConfigurationKey = "ConnectionStrings:Default";

    protected virtual string? DefaultJsonConfigurationFileName => null;

    /// <exception cref="DesignTimeDbContextFactoryException"/>
    public TContext CreateDbContext(string[] args)
    {
        var configuration = MakeConfiguration(args);

        string connectionString = configuration[ConnectionStringConfigurationKey]
            ?? throw new DesignTimeDbContextFactoryException(GetExceptionMessage());

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        ConfigureDbContextOptions(optionsBuilder, connectionString);
        
        return InstantiateDbContext(optionsBuilder.Options);
    }

    protected abstract void ConfigureDbContextOptions(DbContextOptionsBuilder<TContext> optionsBuilder, string connectionString);

    protected abstract TContext InstantiateDbContext(DbContextOptions<TContext> options);

    private IConfigurationRoot MakeConfiguration(string[] args)
    {
        var mainConfigurationBuilder = new ConfigurationBuilder();

        var commandLineConfiguration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        mainConfigurationBuilder.AddConfiguration(commandLineConfiguration);

        string? jsonConfigurationFileName = commandLineConfiguration[JsonConfigurationFileNameConfigurationKey]
            ?? DefaultJsonConfigurationFileName;

        if (jsonConfigurationFileName is not null)
        {
            var jsonConfiguration = new ConfigurationBuilder()
                .AddJsonFile(jsonConfigurationFileName, optional: true)
                .Build();

            mainConfigurationBuilder.AddConfiguration(jsonConfiguration);
        }

        return mainConfigurationBuilder.Build();
    }

    private string GetExceptionMessage()
    {
        string mainMessage = $"No value for configuration key \"{ConnectionStringConfigurationKey}\" found in configuration."
            + " You can pass it through the command line or by providing a configuration .json file name"
            + $" through the \"{JsonConfigurationFileNameConfigurationKey}\" configuration key";
        
        string defaultFileNameMessage = DefaultJsonConfigurationFileName is not null
            ? $" (set to \"{DefaultJsonConfigurationFileName}\" by default)"
            : string.Empty;

        return mainMessage + defaultFileNameMessage;
    }
}