namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public abstract class Profile : Entity
{
    public Profile(Guid accountId, Name name)
    {
        AccountId = accountId;
        Name = name;
    }

    public Guid AccountId { get; set; } 

    public Name Name { get; set; }
}