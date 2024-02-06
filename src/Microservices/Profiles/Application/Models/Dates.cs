using InnowiseClinic.Microservices.Profiles.Application.Exceptions;

namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public static class Dates
{
    public static readonly DateOnly Min = new(1800, 1, 1);
    public static readonly DateOnly Max = DateOnly.MaxValue;

    public static bool IsYearInRange(int year)
    {
        return year >= Min.Year && year <= Max.Year;
    }

    public static void ThrowIfYearIsOutOfRange(int year)
    {
        if (!IsYearInRange(year))
        {
            throw new YearOutOfRangeException(year, Min.Year, Max.Year);
        }
    }

    public static bool IsDateInRange(DateOnly date)
    {
        return date >= Min && date <= Max;
    }

    public static void ThrowIfDateIsOutOfRange(DateOnly date)
    {
        if (!IsDateInRange(date))
        {
            throw new DateOutOfRangeException(date, Min, Max);
        }
    }
}