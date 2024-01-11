namespace InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

public interface ISqlValueFormatter
{
    string FormatToSql(object? value);
}