using System.Text.Json.Serialization;
using InnowiseClinic.Services.Authorization.API.ErrorHandling.ActionOutputVerification;

namespace InnowiseClinic.Services.Authorization.API.Auth.DataTransfer;

[ActionOutput]
public record TokenPairOutput(
    [property: JsonPropertyName("access_token")]
    string AccessToken,
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken,
    [property: JsonPropertyName("token_type")]
    string TokenType,
    [property: JsonPropertyName("expires_in")]
    int ExpiresInSeconds);