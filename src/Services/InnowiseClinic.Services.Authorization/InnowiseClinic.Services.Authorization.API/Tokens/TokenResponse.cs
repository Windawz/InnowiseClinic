using System.Text.Json.Serialization;

namespace InnowiseClinic.Services.Authorization.API.Tokens;

/// <summary>
/// The body of a response to a request for an access or a refresh token.
/// </summary>
/// <remarks>
/// Instances of this type are safe to serialize to JSON.
/// Standard names for token responses will be used,
/// like "access_token" or "expires_in".
/// </remarks>
/// <param name="AccessToken">An encoded access token. For example, a signed JWT token.</param>
/// <param name="RefreshToken">An encoded refresh token. For example, a signed JWT token.</param>
/// <param name="TokenType">The authentication type of the tokens. For example, "bearer".</param>
/// <param name="ExpiresIn">Time after which the access token will have expired. Serialized to seconds.</param>
internal record TokenResponse(
    [property: JsonPropertyName("access_token")]
    string AccessToken,
    [property: JsonPropertyName("refresh_token")]
    string RefreshToken,
    [property: JsonPropertyName("token_type")]
    string TokenType,
    [property: JsonPropertyName("expires_in")]
    [property: JsonConverter(typeof(ExpiresInJsonConverter))]
    TimeSpan ExpiresIn);