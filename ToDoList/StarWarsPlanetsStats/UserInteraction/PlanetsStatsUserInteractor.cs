using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.UserInteraction;

public class PlanetsStatsUserInteractor : IPlanetStatsUserIteractor
{
    private readonly IUserInteractor _userInteractor;

    public PlanetsStatsUserInteractor(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThaCanBeChosen)
    {
        _userInteractor.ShowMassage(Environment.NewLine);
        _userInteractor.ShowMassage("The statistics of which property would you like to see?");
        _userInteractor.ShowMassage(string.Join(Environment.NewLine, propertiesThaCanBeChosen));

        return _userInteractor.ReadFromUser();
    }

    public void Show(IEnumerable<Planet> planets)
    {
        TablePrinter.Print(planets);
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public static class TablePrinter
{
    public static void Print<T>(IEnumerable<T> items)
    {
        const int columnWidth = 15;

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            Console.Write($"{{0, -{columnWidth}}}|", property.Name);
        }

        Console.WriteLine();
        Console.WriteLine(new string('-', properties.Length * (columnWidth + 1)));
        Console.WriteLine();

        foreach (var item in items)
        {
            foreach (var property in properties)
            {
                Console.Write($"{{0, -{columnWidth}}}|", property.GetValue(item));
            }

            Console.WriteLine();
        }
    }
}
