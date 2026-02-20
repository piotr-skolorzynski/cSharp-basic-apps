namespace StarWarsPlanetsStats.Extensions;

public static class StringExtensions
{
    public static int? ToIntOrNull(this string input)
    {
        return int.TryParse(input, out int parsedInput) ? parsedInput : null;
    }
}
