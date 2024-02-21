namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    private object?[]? _cachedEqualityValues = null;

    public virtual bool Equals(ValueObject? other)
    {
        if (other is null)
        {
            return false;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        var values = GetEqualityValues();
        var otherValues = other.GetEqualityValues();

        return values.SequenceEqual(otherValues);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (object? value in GetEqualityValues())
        {
            hashCode.Add(value ?? 0);
        }

        return hashCode.ToHashCode();
    }

    protected virtual object?[] GetEqualityValues()
    {
        return _cachedEqualityValues ??= GetType()
            .GetProperties()
            .Select(property => property.GetValue(this))
            .ToArray();
    }
}