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
            // Filter first just in case, to not "scare away"
            // the case-insensitive collation.
            //
            // May need to enforce ordinal caseless comparison
            // and rethink the collation-utilizing approach
            // if this ends up not working as expected.
            .Where(role => role.Name == name)
            .AsNoTracking()
            .FirstOrDefault();
        
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