namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class YearOutOfRangeException(int year, int min, int max)
    : Exception($"Year {year} may not be out of range of valid years between {min} and {max}");