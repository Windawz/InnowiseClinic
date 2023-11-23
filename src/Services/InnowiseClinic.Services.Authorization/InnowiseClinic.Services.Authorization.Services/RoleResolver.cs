using InnowiseClinic.Services.Authorization.Data;
using InnowiseClinic.Services.Authorization.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace InnowiseClinic.Services.Authorization.Services;

public class RoleResolver : IRoleResolver
{
    private readonly AuthorizationDbContext _dbContext;

    public RoleResolver(AuthorizationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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