using InnowiseClinic.Services.Authorization.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InnowiseClinic.Services.Authorization.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            // Binding from user claims.
            options.ValueProviderFactories.Add(
                new ClaimsValueProviderFactory());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();
        builder.Services.AddAuthorization();

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
