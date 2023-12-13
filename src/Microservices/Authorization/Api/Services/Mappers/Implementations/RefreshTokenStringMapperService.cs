using System.Globalization;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Models;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;

public class RefreshTokenStringMapperService(IRoleMapperService roleMapperService) : IRefreshTokenStringMapperService
{
    private const char _valueSeparator = '.';
    private static readonly CultureInfo _dateTimeFormat = CultureInfo.InvariantCulture;

    public string MapFromRefreshToken(RefreshToken refreshToken)
    {
        return Base64UrlEncoder.Encode(
            string.Join(
                _valueSeparator,
                refreshToken.TokenId.ToString(),
                roleMapperService.MapToRoleName(refreshToken.Role),
                refreshToken.CreatedAt.ToString(_dateTimeFormat),
                refreshToken.ExpiresAt.ToString(_dateTimeFormat)));
    }

    public RefreshToken MapToRefreshToken(string refreshTokenString)
    {
        var valueStrings = Base64UrlEncoder
            .Decode(refreshTokenString)
            .Split(
                separator: _valueSeparator,
                count: 4,
                options: StringSplitOptions.RemoveEmptyEntries 
                    | StringSplitOptions.TrimEntries);

        return new(
            TokenId: Guid.Parse(valueStrings[0]),
            Role: roleMapperService.MapFromRoleName(valueStrings[1]),
            CreatedAt: DateTime.Parse(valueStrings[2], _dateTimeFormat),
            ExpiresAt: DateTime.Parse(valueStrings[3], _dateTimeFormat));
    }
}