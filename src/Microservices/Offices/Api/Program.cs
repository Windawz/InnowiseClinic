using FluentValidation;
using InnowiseClinic.Microservices.Offices.Application.Services.Implementations;
using InnowiseClinic.Microservices.Offices.Application.Services.Interfaces;
using InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Implementations;
using InnowiseClinic.Microservices.Offices.Application.Services.Mappers.Interfaces;
using InnowiseClinic.Microservices.Offices.Data.Contexts;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Implementations;
using InnowiseClinic.Microservices.Offices.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Configuration;
using InnowiseClinic.Microservices.Shared.Api.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace InnowiseClinic.Microservices.Offices.Api;

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
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services
            .AddScoped<IOfficeService, OfficeService>()
            .AddScoped<IOfficeRepository, OfficeRepository>()
            .AddDbContext<OfficesDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseNpgsql(
                    builder.Configuration.GetConnectionString("Default"));
            });

        builder.Services
            .AddScoped<IOfficeMapperService, OfficeMapperService>();

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

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
