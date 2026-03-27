using System;

namespace Restaurants.Exceptions;

public class NotFoundException : Exception
{
    public string ResourceName { get; }
    public object ResourceKey { get; }

    public NotFoundException(string resourceName, object resourceKey)
        : base($"{resourceName} with identifier '{resourceKey}' was not found.")
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }
}
