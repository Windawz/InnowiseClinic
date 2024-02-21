namespace InnowiseClinic.Microservices.Profiles.Application.Models;

public abstract class Entity : IEquatable<Entity>
{
    private Guid _id;

    public Guid? Id => _id == default
        ? null
        : _id;

    public bool IsTransient =>
        _id == default;

    public static void SetId(Entity entity, Guid id)
    {
        entity._id = id;
    }

    public static void ResetId(Entity entity)
    {
        entity._id = default;
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (!ReferenceEquals(this, other))
        {
            return false;
        }

        if (GetType() != other.GetType())
        {
            return false;
        }

        if (IsTransient || other.IsTransient)
        {
            return false;
        }

        return _id == other._id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return IsTransient
            ? base.GetHashCode()
            : _id.GetHashCode();
    }
}