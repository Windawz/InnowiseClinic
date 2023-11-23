using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.Services;

/// <summary>
/// A default implementation of <see cref="IRoleResolver"/>.
/// </summary>
public class RoleResolver : IRoleResolver
{
    private readonly AuthorizationDbContext _dbContext;

    /// <summary>
    /// Creates an instance of <see cref="RoleResolver"/>.
    /// </summary>
    /// <param name="dbContext">An instance of <see cref="AuthorizationDbContext"/>.</param>
    public RoleResolver(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Role? ResolveByName(string name)
    {
        var dataRole = _dbContext.Roles
            .SingleOrDefault(role => role.Name == name);
        
        if (dataRole is not null)
        {
            return Mapping.FromDataRole(dataRole);
        }
        else
        {
            return null;
        }
    }
}