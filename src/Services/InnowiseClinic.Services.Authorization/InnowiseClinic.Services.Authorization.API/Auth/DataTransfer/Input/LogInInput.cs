using System.Text.Json.Serialization;

namespace InnowiseClinic.Services.Authorization.API.Auth.DataTransfer.Input;

public record LogInInput(
    [property: JsonPropertyName("email")]
    string EmailAddress,
    [property: JsonPropertyName("password")]
    string PasswordText);