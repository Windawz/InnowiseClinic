using InnowiseClinic.Services.Authorization.API.Auth.DataTransfer;
using InnowiseClinic.Services.Authorization.API.Auth.Models;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Mapping;

public static class Mapping
{
    public static TokenPairOutput MapToAPILayer(this TokenPair tokenPair)
    {
        return new TokenPairOutput(
            tokenPair.AccessToken,
            tokenPair.RefreshToken,
            tokenPair.TokenType,
            (int)tokenPair.ExpiresIn.TotalSeconds);
    }

    public static (Email, Password) MapToServiceLayer(this RegisterSelfInput input)
    {
        return (new Email(input.EmailAddress), new Password(input.PasswordText));
    }

    public static (Email, Password, Role) MapToServiceLayer(this RegisterOtherInput input)
    {
        return (new Email(input.EmailAddress), new Password(input.PasswordText), new Role(input.RoleName));
    }

    public static (Email, Password) MapToServiceLayer(this LogInInput input)
    {
        return (new Email(input.EmailAddress), new Password(input.PasswordText));
    }
}