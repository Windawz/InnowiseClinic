using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Application.Options;

public class AccessTokenServiceOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required SecurityKey SecurityKey { get; set; }
    public required string Algorithm { get; set; }
    public required int ExpirationSeconds { get; set; }
    public required string RoleClaimType { get; set; }
}