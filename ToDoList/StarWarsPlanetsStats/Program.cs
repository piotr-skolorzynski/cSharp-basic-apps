using StarWarsPlanetsStats.ApiDataAccess;
using StarWarsPlanetsStats.App;

try
{
    await new StarWarsPlanetStatsApp(new ApiDataReader(), new MockStarWarsApiDataReader()).Run();
}
catch (Exception ex)
{
    Console.WriteLine("An error occured. Exception message: " + ex.Message);
}

Console.ReadKey();
