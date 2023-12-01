using InnowiseClinic.Services.Authorization.API.DataTransfer;
using InnowiseClinic.Services.Authorization.Services.Models;

namespace InnowiseClinic.Services.Authorization.API.Models;

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
}