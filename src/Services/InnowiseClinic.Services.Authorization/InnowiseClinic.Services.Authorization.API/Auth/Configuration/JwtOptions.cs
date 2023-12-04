namespace InnowiseClinic.Services.Authorization.API.Auth.Configuration;

public class JwtOptions
{
    public const string ConfigurationKey = "Auth:Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SigningKey { get; set; } = string.Empty;

    public GenerationOptions Generation { get; set; } = new()
    {
        AccessTokenExpirationSeconds = 30,
        RefreshTokenExpirationSeconds = 3600,
    };

    public class GenerationOptions
    {
        public int AccessTokenExpirationSeconds { get; set; }
    
        public int RefreshTokenExpirationSeconds { get; set; }
    }
}