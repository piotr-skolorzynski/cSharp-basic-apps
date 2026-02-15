var numbers = new List<int> { 10, 12, -100, 55, 17, 22 };
var filteringStrategySelector = new FilteringStrategySelector();

Console.WriteLine("Select filter:");
Console.WriteLine(string.Join(", ", filteringStrategySelector.FilteringStrategyNames));

var userInput = Console.ReadLine();
var filteringStrategy = new FilteringStrategySelector().Select(userInput);
var result = new Filter().FilterBy(filteringStrategy, numbers);

Print(result);

Console.ReadKey();

void Print(IEnumerable<int> numbers)
{
    Console.WriteLine(string.Join(", ", numbers));
}

public class Filter
{
    public IEnumerable<T> FilterBy<T>(Func<T, bool> predicate, IEnumerable<T> items)
    {
        var result = new List<T>();
        foreach (var item in items)
        {
            if (predicate(item))
            {
                result.Add(item);
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
        ["Negative"] = n => n < 0,
    };

    public IEnumerable<string> FilteringStrategyNames => _filteringStrategies.Keys;

    public Func<int, bool> Select(string filteringType)
    {
        if (!_filteringStrategies.ContainsKey(filteringType))
        {
            throw new ArgumentException($"Filtering type {filteringType} is not supported.");
        }

        return _filteringStrategies[filteringType];
    }
}
