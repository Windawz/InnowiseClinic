using System.Security.Claims;
using InnowiseClinic.Services.Authorization.API.Infrastructure;

namespace InnowiseClinic.Services.Authorization.API.Models;

public record RegisterOtherInput(
    [FromClaims(ClaimTypes.NameIdentifier)] int InitiatorId,
    string EmailAddress,
    string PasswordText,
    string RoleName);