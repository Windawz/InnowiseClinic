using InnowiseClinic.Microservices.Profiles.Application.Models;

namespace InnowiseClinic.Microservices.Profiles.Application.Repositories.Filtering;

public class FilteredName
{
    public FilteredName(Name name, bool filterFirst = false, bool filterLast = false, bool filterMiddle = false)
    {
        First = filterFirst
            ? name.First
            : null;
        
        Last = filterLast
            ? name.Last
            : null;

        Middle = filterMiddle
            ? name.Middle
            : null;
    }

    public FilteredName(string? first = null, string? last = null, string? middle = null)
    {
        First = first;
        Last = last;
        Middle = middle;
    }

    public string? First { get; }
    public string? Last { get; }
    public string? Middle { get; }
}