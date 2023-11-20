using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InnowiseClinic.Services.Authorization.Data;

/// <summary>
/// Creates an instance of <see cref="AuthorizationDbContext"/> when creating migrations and performing
/// other design-time work.
/// </summary>
internal class AuthorizationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthorizationDbContext>
{
    /// <inheritdoc/>
    /// <exception cref="AuthorizationDbContextDesignTimeException">
    /// Thrown if context creation fails for whatever reason.
    /// </exception>
    public AuthorizationDbContext CreateDbContext(string[] args)
    {
        if (args.Length < 1)
        {
            throw new AuthorizationDbContextDesignTimeException(
                "Missing the connection string argument. "
                + "You can pass the connection string to the design-time factory "
                + "by adding \'--\' after the command and then "
                + "the connection string in double quotes (\"\")");
        }

        string connectionString = args[0].Trim();

        var optionsBuilder = new DbContextOptionsBuilder<AuthorizationDbContext>()
            .UseSqlServer(connectionString);
        
        return new AuthorizationDbContext(optionsBuilder.Options);
    }
}