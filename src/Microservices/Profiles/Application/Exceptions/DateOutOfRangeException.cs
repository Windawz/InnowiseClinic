namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class DateOutOfRangeException(DateOnly date, DateOnly min, DateOnly max)
    : Exception($"Date {date} may not be out of range of valid dates between {min} and {max}");