using System.Security.Claims;
using System.Text.Json.Serialization;
using InnowiseClinic.Services.Authorization.API.Binding;

namespace InnowiseClinic.Services.Authorization.API.DataTransfer;

public record RegisterOtherInput(
    [FromClaims(ClaimTypes.NameIdentifier)]
    int InitiatorId,
    [property: JsonPropertyName("email")]
    string EmailAddress,
    [property: JsonPropertyName("password")]
    string PasswordText,
    [property: JsonPropertyName("role")]
    string RoleName);