using System.Globalization;
using InnowiseClinic.Microservices.Authorization.Api.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Api.Mapping;

public static class RefreshTokenStringMapping
{
    private const char _refreshTokenStringValueSeparator = '.';
    private static readonly CultureInfo _refreshTokenDateTimeFormat = CultureInfo.InvariantCulture;

    public static string ToString(RefreshToken refreshToken)
    {
        return Base64UrlEncoder.Encode(
            string.Join(
                _refreshTokenStringValueSeparator,
                refreshToken.TokenId.ToString(),
                RoleNameMapping.ToRoleName(refreshToken.Role),
                refreshToken.CreatedAt.ToString(_refreshTokenDateTimeFormat),
                refreshToken.ExpiresAt.ToString(_refreshTokenDateTimeFormat)));
    }

    public static RefreshToken ToRefreshToken(string refreshTokenString)
    {
        refreshTokenString = refreshTokenString.Trim();

        string decodedString;

        try
        {
            decodedString = Base64UrlEncoder.Decode(refreshTokenString);
        }
        catch (FormatException)
        {
            throw new InvalidRefreshTokenFormatException(refreshTokenString);
        }

        var valueStrings = decodedString.Split(
            separator: _refreshTokenStringValueSeparator,
            count: 4,
            options: StringSplitOptions.RemoveEmptyEntries 
                | StringSplitOptions.TrimEntries);

        Guid tokenId;
        DateTime createdAt;
        DateTime expiresAt;

        try
        {
            tokenId = Guid.Parse(valueStrings[0]);
            createdAt = DateTime.Parse(valueStrings[2], _refreshTokenDateTimeFormat);
            expiresAt = DateTime.Parse(valueStrings[3], _refreshTokenDateTimeFormat);
        }
        catch (FormatException)
        {
            throw new InvalidRefreshTokenFormatException(refreshTokenString);
        }

        return new(
            TokenId: tokenId,
            Role: RoleNameMapping.ToRole(valueStrings[1]),
            CreatedAt: createdAt,
            ExpiresAt: expiresAt);
    }
}