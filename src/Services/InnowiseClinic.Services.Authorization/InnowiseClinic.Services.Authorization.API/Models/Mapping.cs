using InnowiseClinic.Services.Authorization.API.DataTransfer;

namespace InnowiseClinic.Services.Authorization.API.Models;

public static class Mapping
{
    public static TokenPairOutput ToDataTransferTokenPair(this TokenPair tokenPair)
    {
        return new TokenPairOutput(
            tokenPair.AccessToken,
            tokenPair.RefreshToken,
            tokenPair.TokenType,
            (int)tokenPair.ExpiresIn.TotalSeconds);
    }
}