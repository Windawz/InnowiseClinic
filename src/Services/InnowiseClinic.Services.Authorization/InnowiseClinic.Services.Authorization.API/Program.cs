using InnowiseClinic.Services.Authorization.API.Binding;
using InnowiseClinic.Services.Authorization.API.Composition;
using InnowiseClinic.Services.Authorization.API.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InnowiseClinic.Services.Authorization.API;

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
        
        builder.Services.AddAuthorization();
        
        builder.AddCustomComponents();
        
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddLogging();
        }


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
