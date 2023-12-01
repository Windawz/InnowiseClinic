namespace InnowiseClinic.Services.Authorization.API.Auth.Models;

public record TokenPair(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    TimeSpan ExpiresIn);