using System.Text.Json.Serialization;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Auth.DataTransfer;

public record RegisterOtherInput(
    [property: JsonPropertyName("email")]
    string EmailAddress,
    [property: JsonPropertyName("password")]
    string PasswordText,
    [property: JsonPropertyName("role")]
    string RoleName);
