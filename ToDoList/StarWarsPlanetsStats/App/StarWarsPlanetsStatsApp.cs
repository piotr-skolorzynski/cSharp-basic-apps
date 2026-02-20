using System.Text.Json;
using StarWarsPlanetsStats.ApiDataAccess;
using StarWarsPlanetsStats.DTOs;

namespace StarWarsPlanetsStats.App;

public class StarWarsPlanetStatsApp
{
    private string baseAddress = "https://swapi.dev/api/";
    private string requestUri = "planets";

    private readonly IApiDataReader _apiReader;
    private readonly IApiDataReader _secondaryApiReader;

    public StarWarsPlanetStatsApp(IApiDataReader apiReader, IApiDataReader secondaryApiReader)
    {
        _apiReader = apiReader;
        _secondaryApiReader = secondaryApiReader;
    }

    public async Task Run()
    {
        string? json = null;
        try
        {
            json = await _apiReader.Read(baseAddress, requestUri);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(
                $"API request was unsuccessful. Switching to mock data. Exception message: {ex.Message} "
            );
        }

        json ??= await _secondaryApiReader.Read(baseAddress, requestUri);

        var root = JsonSerializer.Deserialize<Root>(json);

        var planets = ToPlanets(root);

        var propertyNamesToSelectorsMapping = new Dictionary<string, Func<Planet, int?>>()
        {
            ["population"] = planet => planet.Population,
            ["diameter"] = planet => planet.Diameter,
            ["surface water"] = planet => planet.SurfaceWater,
        };

        Console.WriteLine();
        Console.WriteLine("The statistics of which property would you like to see?");
        Console.WriteLine(string.Join(Environment.NewLine, propertyNamesToSelectorsMapping.Keys));

        var userChoice = Console.ReadLine();

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

    private static IEnumerable<Planet> ToPlanets(Root? root)
    {
        if (root is null)
        {
            throw new ArgumentNullException(nameof(root));
        }

        return root.results.Select(planetDto => (Planet)planetDto);
    }
}

public readonly record struct Planet
{
    public string Name { get; }
    public int Diameter { get; }
    public int? SurfaceWater { get; }
    public int? Population { get; }

    public Planet(string name, int diameter, int? surfaceWater, int? population)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
        Diameter = diameter;
        SurfaceWater = surfaceWater;
        Population = population;
    }

    public static explicit operator Planet(Result planetDto)
    {
        var name = planetDto.name;
        var diameter = int.Parse(planetDto.diameter);
        int? surfaceWater = planetDto.surface_water.ToIntOrNull();
        int? population = planetDto.population.ToIntOrNull();

        return new Planet(name, diameter, surfaceWater, population);
    }
}

public static class StringExtensions
{
    public static int? ToIntOrNull(this string input)
    {
        return int.TryParse(input, out int parsedInput) ? parsedInput : null;
    }
}
