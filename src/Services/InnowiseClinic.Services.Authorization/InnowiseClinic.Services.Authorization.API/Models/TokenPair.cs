namespace InnowiseClinic.Services.Authorization.API.Models;

public record TokenPair(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    TimeSpan ExpiresIn);