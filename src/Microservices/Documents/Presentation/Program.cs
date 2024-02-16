using Azure.Storage;
using Azure.Storage.Blobs;
using InnowiseClinic.Microservices.Documents.Presentation.Services;
using InnowiseClinic.Microservices.Shared.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace InnowiseClinic.Microservices.Documents.Presentation;

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

        builder.Services.AddScoped<IDocumentContainer, DocumentContainer>();
        builder.Services.AddScoped(serviceProvider =>
        {
            // Default Azurite account.
            const string accountName = "devstoreaccount1";
            const string accountKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";

            var uri = new Uri($"http://127.0.0.1:10000/{accountName}");
            var credential = new StorageSharedKeyCredential(accountName, accountKey);

            return new BlobServiceClient(uri, credential);
        });

        builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

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
