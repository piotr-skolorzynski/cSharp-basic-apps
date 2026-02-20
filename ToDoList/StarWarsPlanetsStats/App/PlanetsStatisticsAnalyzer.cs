using StarWarsPlanetsStats.Models;
using StarWarsPlanetsStats.UserInteraction;

namespace StarWarsPlanetsStats.App;

public class PlanetsStatisticsAnalyzer : IPlanetsStatisticsAnalyzer
{
    private readonly IPlanetStatsUserIteractor _planetsStatsUserInteractor;

    public PlanetsStatisticsAnalyzer(IPlanetStatsUserIteractor planetsStatsUserInteractor)
    {
        _planetsStatsUserInteractor = planetsStatsUserInteractor;
    }

    public void Analyze(IEnumerable<Planet> planets)
    {
        var propertyNamesToSelectorsMapping = new Dictionary<string, Func<Planet, int?>>()
        {
            ["population"] = planet => planet.Population,
            ["diameter"] = planet => planet.Diameter,
            ["surface water"] = planet => planet.SurfaceWater,
        };

        var userChoice = _planetsStatsUserInteractor.ChooseStatisticsToBeShown(
            propertyNamesToSelectorsMapping.Keys
        );

        if (userChoice is null || !propertyNamesToSelectorsMapping.ContainsKey(userChoice))
        {
            Console.WriteLine("Invalid choice!");
        }
        else
        {
            ShowStatistics(planets, userChoice, propertyNamesToSelectorsMapping[userChoice]);
        }
    }

    private static void ShowStatistics(
        IEnumerable<Planet> planets,
        string propertyName,
        Func<Planet, int?> propertySelector
    )
    {
        ShowStatistics("Max", planets.MaxBy(propertySelector), propertySelector, propertyName);
        ShowStatistics("Min", planets.MinBy(propertySelector), propertySelector, propertyName);
    }

    private static void ShowStatistics(
        string descriptor,
        Planet selectedPlanet,
        Func<Planet, int?> propertySelector,
        string propertyName
    )
    {
        Console.WriteLine(
            $"{descriptor} {propertyName} is: {propertySelector(selectedPlanet)} (planet: {selectedPlanet.Name})"
        );
    }
}
