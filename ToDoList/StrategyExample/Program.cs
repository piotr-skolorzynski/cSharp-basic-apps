var numbers = new List<int> { 10, 12, -100, 55, 17, 22 };

Console.WriteLine(
    @"Select filter:
Even
Odd
Positive
"
);
var input = Console.ReadLine();
var result = new NumbersFilter().FilterBy(input, numbers);

Print(result);

Console.ReadKey();

void Print(List<int> numbers)
{
    Console.WriteLine(string.Join(", ", numbers));
}

public class NumbersFilter
{
    public List<int> FilterBy(string filteringType, List<int> numbers)
    {
        switch (filteringType)
        {
            case "Even":
                return Select(numbers, n => n % 2 == 0);
            case "Odd":
                return Select(numbers, n => n % 2 == 1);
            case "Positive":
                return Select(numbers, n => n > 0);
            default:
                throw new NotSupportedException($"{filteringType} is not a valid filter");
        }
    }

    private List<int> Select(List<int> numbers, Func<int, bool> predicate)
    {
        var result = new List<int>();
        foreach (var number in numbers)
        {
            if (predicate(number))
            {
                result.Add(number);
            }
        }

        return result;
    }
}
