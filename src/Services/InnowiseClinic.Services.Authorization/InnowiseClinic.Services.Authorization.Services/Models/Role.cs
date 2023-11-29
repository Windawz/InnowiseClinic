namespace InnowiseClinic.Services.Authorization.Services.Models;

public record Role
{
    private readonly string _name = null!;

    public Role(string roleName)
    {
        Name = roleName;
    }

    public string Name
    {
        get => _name;
        init
        {
            if (!IsValid(value))
            {
                throw new InvalidRoleNameException(value);
            }
            _name = value.ToLowerInvariant();
        }
    }

    public static bool IsValid(string roleName)
    {
        return roleName is RoleNames.Patient or RoleNames.Doctor or RoleNames.Receptionist;
    }
}