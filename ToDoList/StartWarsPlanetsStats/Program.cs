using System.Text.Json;
using StartWarsPlanetsStats.ApiDataAccess;
using StartWarsPlanetsStats.DTOs;

var baseAddress = "https://swapi.dev/api/";
var requestUri = "planets";
var apiReader = new ApiDataReader();
string json = await apiReader.Read(baseAddress, requestUri);

var root = JsonSerializer.Deserialize<Root>(json);

if (root is not null)
{
    foreach (var item in root.results)
    {
        Console.WriteLine($"Planet name: {item.name}, climate: {item.climate}");
    }
}

Console.ReadKey();
