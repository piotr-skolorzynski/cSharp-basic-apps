namespace StartWarsPlanetsStats.App;

public class StarWarsPlanetStatsApp
{
    private string baseAddress = "https://swapi.dev/api/";
    private string requestUri = "planets";

    public async Task Run()
    {
        string? json = null;
        try
        {
            IApiDataReader apiReader = new ApiDataReader();
            json = await apiReader.Read(baseAddress, requestUri);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(
                $"API request was unsuccessful. Switching to mock data. Exception message: {ex.Message} "
            );
        }

        if (json is null)
        {
            IApiDataReader apiDataReader = new MockStarWarsApiDataReader();
            json = await apiDataReader.Read(baseAddress, requestUri);
        }

        var root = JsonSerializer.Deserialize<Root>(json);
    }
}
