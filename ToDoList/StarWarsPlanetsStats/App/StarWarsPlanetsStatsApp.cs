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
    }
}
