var numbers = new List<int> { 10, 12, -100, 55, 17, 22 };

Console.WriteLine(
    @"Select filter:
Even
Odd
Positive
"
);

var userInput = Console.ReadLine();
var filteringStrategy = new FilteringStrategySelector().Select(userInput);
var result = new NumbersFilter().FilterBy(filteringStrategy, numbers);

Print(result);

Console.ReadKey();

void Print(List<int> numbers)
{
    Console.WriteLine(string.Join(", ", numbers));
}

public class NumbersFilter
{
    public List<int> FilterBy(Func<int, bool> predicate, List<int> numbers)
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

public class FilteringStrategySelector
{
    private readonly Dictionary<string, Func<int, bool>> _filteringStrategies = new Dictionary<
        string,
        Func<int, bool>
    >
    // { alternatywne sposoby inicjalizacji słownika:
    //     { "Even", n => n % 2 == 0 },
    //     { "Odd", n => n % 2 == 1 },
    //     { "Positive", n => n > 0 },
    // };
    {
        ["Even"] = n => n % 2 == 0,
        ["Odd"] = n => n % 2 == 1,
        ["Positive"] = n => n > 0,
    };

    public Func<int, bool> Select(string filteringType)
    {
        if (!_filteringStrategies.ContainsKey(filteringType))
        {
            throw new ArgumentException($"Filtering type {filteringType} is not supported.");
        }

        return _filteringStrategies[filteringType];
    }
}
