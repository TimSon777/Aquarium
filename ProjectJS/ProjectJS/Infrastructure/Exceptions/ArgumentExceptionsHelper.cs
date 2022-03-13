namespace ProjectJS.Infrastructure.Exceptions;

public static class ArgumentExceptionsHelper
{
    public static void ThrowWhenNotPositive(this int val, string name)
    {
        if (val <= 0)
        {
            throw new ArgumentException("Value s=must be positive", name);
        } 
    }
}