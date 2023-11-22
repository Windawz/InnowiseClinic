namespace InnowiseClinic.Services.Authorization.Services.Models;

/// <summary>
/// Information necessary to be provided for account registration.
/// </summary>
/// <param name="Email">Account email.</param>
/// <param name="Password">Account password in plain text.</param>
public record RegistrationInfo(
    string Email,
    string Password);