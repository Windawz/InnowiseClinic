namespace InnowiseClinic.Microservices.Shared.Utilities.Mapping;

public static class Map
{
    public static Mapper<TOut> To<TOut>() =>
        new();
}