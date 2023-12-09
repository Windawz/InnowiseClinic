namespace InnowiseClinic.Microservices.Authorization.Application.Options;

public class RefreshTokenServiceOptions
{
    public required int ExpirationSeconds { get; set; }
}