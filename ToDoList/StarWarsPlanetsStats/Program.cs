using StarWarsPlanetsStats.ApiDataAccess;
using StarWarsPlanetsStats.App;
using StarWarsPlanetsStats.DataAccess;
using StarWarsPlanetsStats.UserInteraction;

try
{
    await new StarWarsPlanetStatsApp(
        new PlanetsFromApiReader(new ApiDataReader(), new MockStarWarsApiDataReader()),
        new PlanetsStatisticsAnalyzer(new PlanetsStatsUserInteractor(new ConsoleUserInteractor()))
    ).Run();
}
catch (Exception ex)
{
    Console.WriteLine("An error occured. Exception message: " + ex.Message);
}

Console.ReadKey();
