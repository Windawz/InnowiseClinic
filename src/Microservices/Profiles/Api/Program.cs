using FluentValidation;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Requests;
using InnowiseClinic.Microservices.Profiles.Api.DataTransferObjects.Targets;
using InnowiseClinic.Microservices.Profiles.Api.Validators;
using InnowiseClinic.Microservices.Profiles.Application.Exceptions;
using InnowiseClinic.Microservices.Profiles.Application.Repositories;
using InnowiseClinic.Microservices.Profiles.Data.Repositories;
using InnowiseClinic.Microservices.Shared.Api.Configuration;
using InnowiseClinic.Microservices.Shared.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
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
            .AddScoped<IValidator<CreatePatientRequest>, CreatePatientRequestValidator>()
            .AddScoped<IValidator<CreateDoctorRequest>, CreateDoctorRequestValidator>()
            .AddScoped<IValidator<CreateReceptionistRequest>, CreateReceptionistRequestValidator>()
            .AddScoped<IValidator<EditPatientTarget>, EditPatientTargetValidator>()
            .AddScoped<IValidator<EditDoctorTarget>, EditDoctorTargetValidator>()
            .AddScoped<IValidator<EditReceptionistTarget>, EditReceptionistTargetValidator>()
            .AddScoped<IProfileRepository, ProfileRepository>()
            .AddSingleton<IMongoClient, MongoClient>(_ =>
            {
                return new(builder.Configuration.GetConnectionString("Default")
                    ?? throw new InvalidOperationException("No default connection string provided"));   
            })
            .AddScoped(serviceProvider =>
            {
                string databaseName = builder.Configuration
                    .GetRequiredSection("DatabaseName").Value!;

                return serviceProvider.GetRequiredService<IMongoClient>()
                    .GetDatabase(databaseName);
            });

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>(new Dictionary<Type, int>
        {
            [typeof(DateOfBirthExceedsCareerStartYearException)] = StatusCodes.Status400BadRequest,
            [typeof(DateOutOfRangeException)] = StatusCodes.Status400BadRequest,
            [typeof(ProfileNotFoundByIdException)] = StatusCodes.Status404NotFound,
            [typeof(ProfileNotFoundByNameException)] = StatusCodes.Status404NotFound,
            [typeof(ProfileWithGivenNameAlreadyExistsException)] = StatusCodes.Status409Conflict,
            [typeof(YearOutOfRangeException)] = StatusCodes.Status400BadRequest,
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
