using InnowiseClinic.Microservices.Authorization.Api.Configuration;
using InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;
using InnowiseClinic.Microservices.Authorization.Api.Services.Implementations;
using InnowiseClinic.Microservices.Authorization.Api.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Implementations;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InnowiseClinic.Microservices.Authorization.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        builder.Services.AddMappingExceptionHandler(options =>
        {
            options.MapStatusCode<AccountAlreadyExistsException>(StatusCodes.Status409Conflict)
                .MapStatusCode<AccountNotFoundException>(StatusCodes.Status404NotFound)
                .MapStatusCode<InvalidPasswordException>(StatusCodes.Status400BadRequest)
                .MapStatusCode<InvalidRefreshTokenException>(StatusCodes.Status401Unauthorized)
                .MapStatusCode<UnknownRoleException>(StatusCodes.Status400BadRequest);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();

        builder.Services
            .AddScoped<ILogInService, LogInService>()
            .AddScoped<IPasswordHashingService, PasswordHashingService>()
            .AddScoped<IRefreshService, RefreshService>()
            .AddScoped<IRegisterService, RegisterService>()
            .AddScoped<IAccessTokenService, AccessTokenService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IRefreshTokenService, RefreshTokenService>();

        builder.Services
            .AddScoped<ILogInRequestMapperService, LogInRequestMapperService>()
            .AddScoped<IRefreshRequestMapperService, RefreshRequestMapperService>()
            .AddScoped<IRefreshTokenStringMapperService, RefreshTokenStringMapperService>()
            .AddScoped<IRegisterRequestMapperService, RegisterRequestMapperService>()
            .AddScoped<ITokenResponseMapperService, TokenResponseMapperService>()
            .AddScoped<IAccountMapperService, AccountMapperService>()
            .AddScoped<IRefreshTokenMapperService, RefreshTokenMapperService>()
            .AddScoped<IRoleMapperService, RoleMapperService>();

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
