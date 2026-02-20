using System.Text.Json;
using StarWarsPlanetsStats.ApiDataAccess;
using StarWarsPlanetsStats.DTOs;
using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.DataAccess;

public class PlanetsFromApiReader : IPlanetsReader
{
    private string baseAddress = "https://swapi.dev/api/";
    private string requestUri = "planets";

    private readonly IApiDataReader _apiReader;
    private readonly IApiDataReader _secondaryApiReader;

    public PlanetsFromApiReader(IApiDataReader apiReader, IApiDataReader secondaryApiReader)
    {
        _apiReader = apiReader;
        _secondaryApiReader = secondaryApiReader;
    }

    public async Task<IEnumerable<Planet>> Read()
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

        return ToPlanets(root);
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
