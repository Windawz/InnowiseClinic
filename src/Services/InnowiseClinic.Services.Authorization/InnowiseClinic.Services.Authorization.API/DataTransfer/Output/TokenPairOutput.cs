using System.Text.Json.Serialization;

namespace InnowiseClinic.Services.Authorization.API.DataTransfer.Output;

public record TokenPairOutput(
    [property: JsonPropertyName("access_token")]
    string AccessToken,
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken,
    [property: JsonPropertyName("token_type")]
    string TokenType,
    [property: JsonPropertyName("expires_in")]
    int ExpiresInSeconds);