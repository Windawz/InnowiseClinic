
using InnowiseClinic.Microservices.Authorization.Api.Configuration.Configurators.Auth;
using InnowiseClinic.Microservices.Authorization.Api.ExceptionHandlers;
using InnowiseClinic.Microservices.Authorization.Application.Services.Exceptions;
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
                .MapStatusCode<InvalidPasswordException>(StatusCodes.Status400BadRequest);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();

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
