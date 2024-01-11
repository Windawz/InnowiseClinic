using InnowiseClinic.Microservices.Profiles.Data.Services.Interfaces;

namespace InnowiseClinic.Microservices.Profiles.Data.Services.Implementations;

public class SqlValueFormatter : ISqlValueFormatter
{
    public string FormatToSql(object? value)
    {
        switch (value)
        {
            case Guid guid:
                return $@"'{guid}'";
            case DateTime dateTime:
                return FormatDateTime(dateTime);
            case DateOnly dateOnly:
                return FormatDateOnly(dateOnly);
            case TimeOnly timeOnly:
                return FormatTimeOnly(timeOnly);
            case null:
                return "NULL";
            default:
                return value.ToString() ??
                    throw new ArgumentException("Value's ToString() method may not return null");
        };
    }

    private static string FormatDateOnly(DateOnly dateOnly)
    {
        return @$"'{dateOnly.Year}-{dateOnly.Month}-{dateOnly.Day}'";
    }

    private static string FormatTimeOnly(TimeOnly timeOnly)
    {
        return @$"'{timeOnly.Hour}:{timeOnly.Minute}:{timeOnly.Second}'";
    }

    private static string FormatDateTime(DateTime dateTime)
    {
        var date = DateOnly.FromDateTime(dateTime);
        var time = TimeOnly.FromDateTime(dateTime);

        return @$"'{FormatDateOnly(date)[1..^1]} {FormatTimeOnly(time)[1..^1]}'";
    }
}