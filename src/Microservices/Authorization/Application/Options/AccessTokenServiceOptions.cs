using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Application.Options;

public class AccessTokenServiceOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required SecurityKey SecurityKey { get; set; }
    public string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
    public int ExpirationSeconds { get; set; } = 30;
    public string RoleClaimType { get; set; } = ClaimTypes.Role;
}