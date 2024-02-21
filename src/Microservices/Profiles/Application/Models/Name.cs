namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public class Name : ValueObject
{
    public Name(string first, string last, string? middle = null)
    {
        First = first;
        Last = last;
        Middle = middle;
    }

    public string First { get; }

    public string Last { get; }

    public string? Middle { get; }
}