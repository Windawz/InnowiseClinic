using System.Text.Json.Serialization;

namespace InnowiseClinic.Services.Authorization.API.DataTransfer.Input;

public record RegisterOtherInput(
    [property: JsonPropertyName("email")]
    string EmailAddress,
    [property: JsonPropertyName("password")]
    string PasswordText,
    [property: JsonPropertyName("role")]
    string RoleName);
