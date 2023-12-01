namespace InnowiseClinic.Services.Authorization.API.Auth.Configuration;

public class JwtOptions
{
    public const string ConfigurationKey = "Auth:Jwt";

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SigningKey { get; set; } = string.Empty;

    public GenerationOptions Generation { get; set; } = null!;

    public class GenerationOptions
    {
        public int AccessTokenExpirationSeconds { get; set; } = 30;
    
        public int RefreshTokenExpirationSeconds { get; set; } = 3600;
    }
}