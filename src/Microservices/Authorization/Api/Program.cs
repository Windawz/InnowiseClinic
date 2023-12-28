using System.IdentityModel.Tokens.Jwt;
using FluentValidation;
using InnowiseClinic.Microservices.Authorization.Api.Configuration;
using InnowiseClinic.Microservices.Authorization.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Authorization.Api.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Implementations;
using InnowiseClinic.Microservices.Authorization.Api.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Api.Validators;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
using InnowiseClinic.Microservices.Authorization.Application.Services.Implementations;
using InnowiseClinic.Microservices.Authorization.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Implementations;
using InnowiseClinic.Microservices.Authorization.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Authorization.Data.Contexts;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Implementations;
using InnowiseClinic.Microservices.Authorization.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Configuration;
using InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

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
                .MapStatusCode<InvalidRefreshTokenFormatException>(StatusCodes.Status400BadRequest);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services
            .AddScoped<IAccessTokenService, AccessTokenService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IRefreshTokenService, RefreshTokenService>()
            .AddScoped<IPasswordHasher<string>, PasswordHasher<string>>()
            .AddScoped<JwtSecurityTokenHandler>()
            .AddDbContext<AuthorizationDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"));
            });

        builder.Services
            .AddScoped<ILogInRequestMapperService, LogInRequestMapperService>()
            .AddScoped<IRefreshRequestMapperService, RefreshRequestMapperService>()
            .AddScoped<IRefreshTokenStringMapperService, RefreshTokenStringMapperService>()
            .AddScoped<IRegisterRequestMapperService, RegisterRequestMapperService>()
            .AddScoped<IRegisterOtherRequestMapperService, RegisterOtherRequestMapperService>()
            .AddScoped<ITokenResponseMapperService, TokenResponseMapperService>()
            .AddScoped<IRoleNameMapperService, RoleNameMapperService>()
            .AddScoped<IAccountMapperService, AccountMapperService>()
            .AddScoped<IRefreshTokenMapperService, RefreshTokenMapperService>()
            .AddScoped<IRoleMapperService, RoleMapperService>();

        builder.Services
            .AddScoped<IValidator<LogInRequest>, LogInRequestValidator>()
            .AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

        builder.Services
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>()
            .ConfigureOptions<ConfigureAccessTokenServiceOptions>()
            .ConfigureOptions<ConfigureRefreshTokenServiceOptions>();

        ValidatorOptions.Global.LanguageManager.Enabled = false;

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
