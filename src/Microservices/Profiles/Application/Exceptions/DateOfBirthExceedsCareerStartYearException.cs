namespace InnowiseClinic.Microservices.Profiles.Application.Exceptions;

public class DateOfBirthExceedsCareerStartYearException(DateOnly dateOfBirth, int careerStartYear)
    : Exception($"Date of birth {dateOfBirth} may not exceed career start year {careerStartYear}");