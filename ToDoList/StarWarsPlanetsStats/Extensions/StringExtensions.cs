namespace StarWarsPlanetsStats.Extensions;

public static class StringExtensions
{
    public static int? ToIntOrNull(this string input)
    {
        return int.TryParse(input, out int parsedInput) ? parsedInput : null;
    }

    public static long? ToLongOrNull(this string input)
    {
        return long.TryParse(input, out long parsedInput) ? parsedInput : null;
    }
}
