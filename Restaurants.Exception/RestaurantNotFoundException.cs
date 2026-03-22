using System;

namespace Restaurants.Exceptions;

public class RestaurantNotFoundException : ArgumentException
{
    public RestaurantNotFoundException() { }

    public RestaurantNotFoundException(string message) : base(message) { }

    public RestaurantNotFoundException(string message, Exception innerException) : base(message, innerException) { }


}
