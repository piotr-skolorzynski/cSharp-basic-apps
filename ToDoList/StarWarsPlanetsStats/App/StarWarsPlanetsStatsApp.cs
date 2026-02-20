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

        if (json is null)
        {
            json = await _secondaryApiReader.Read(baseAddress, requestUri);
        }

        var root = JsonSerializer.Deserialize<Root>(json);

        var planets = ToPlanets(root);

        foreach (var planet in planets)
        {
            Console.WriteLine(planet);
        }

        Console.WriteLine();

        Console.WriteLine("The statistics of which property would you like to see?");
        Console.WriteLine("population");
        Console.WriteLine("diameter");
        Console.WriteLine("surface water");

        var userChoice = Console.ReadLine();

        if (userChoice == "population")
        {
            ShowStatistics(planets, "population", planet => planet.Population);
        }
        else if (userChoice == "diameter")
        {
            ShowStatistics(planets, "diameter", planet => planet.Diameter);
        }
        else if (userChoice == "seurface water")
        {
            ShowStatistics(planets, "surface water", planet => planet.SurfaceWater);
        }
        else
        {
            Console.WriteLine("Invalid choice!");
        }
    }

    private void ShowStatistics(
        IEnumerable<Planet> planets,
        string propertyName,
        Func<Planet, int?> propertySelector
    )
    {
        var planetWithMaxPropertyName = planets.MaxBy(propertySelector);
        Console.WriteLine(
            $"Max {propertyName} is: {propertySelector(planetWithMaxPropertyName)}. Planet: {planetWithMaxPropertyName.Name}"
        );

        var planetWithMinPropertyName = planets.MinBy(propertySelector);
        Console.WriteLine(
            $"Min {propertyName} is: {propertySelector(planetWithMinPropertyName)}. Planet: {planetWithMinPropertyName.Name}"
        );
    }

    private IEnumerable<Planet> ToPlanets(Root? root)
    {
        if (root is null)
        {
            throw new ArgumentNullException(nameof(root));
        }

        var planets = new List<Planet>();
        foreach (var planetDto in root.results)
        {
            Planet planet = (Planet)planetDto;
            planets.Add(planet);
        }

        return planets;
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
        int? result = null;
        if (int.TryParse(input, out int parsedInput))
        {
            result = parsedInput;
        }

        return result;
    }
}
