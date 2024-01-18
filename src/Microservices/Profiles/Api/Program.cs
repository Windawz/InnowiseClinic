using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Data.Entities.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Repositories.Interfaces;
using InnowiseClinic.Microservices.Profiles.Data.Services.Implementations;
using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;
using InnowiseClinic.Microservices.Shared.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace InnowiseClinic.Microservices.Profiles.Api;

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
        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();
        builder.Services.AddFluentValidationAutoValidation();
        
        builder.Services
            .AddScoped<IRepository<PatientEntity>, PatientRepository>()
            .AddScoped<IRepository<DoctorEntity>, DoctorRepository>()
            .AddScoped<IRepository<ReceptionistEntity>, ReceptionistRepository>()
            .AddSingleton<IDbConnectionFactory, DbConnectionFactory>(_ =>
            {
                return new(builder.Configuration.GetConnectionString("Default")
                    ?? throw new InvalidOperationException("No default connection string provided"));   
            })
            .AddSingleton<IEntityMetadataProvider, EntityMetadataProvider>()
            .AddSingleton<ISqlValueFormatter, SqlValueFormatter>();

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
