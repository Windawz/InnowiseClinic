namespace InnowiseClinic.Microservices.Authorization.Application.Options;

public class RefreshTokenServiceOptions
{
    public int ExpirationSeconds { get; set; } = 3600;
}