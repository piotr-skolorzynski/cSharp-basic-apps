using StarWarsPlanetsStats.DataAccess;

namespace StarWarsPlanetsStats.App;

public class StarWarsPlanetStatsApp
{
    private readonly IPlanetsReader _planetsReader;
    private readonly IPlanetsStatisticsAnalyzer _planetsStatisticsAnalyzer;

    public StarWarsPlanetStatsApp(
        IPlanetsReader planetsReader,
        IPlanetsStatisticsAnalyzer planetStatisticsAnalyzer
    )
    {
        _planetsReader = planetsReader;
        _planetsStatisticsAnalyzer = planetStatisticsAnalyzer;
    }

    public async Task Run()
    {
        var planets = await _planetsReader.Read();
        _planetsStatisticsAnalyzer.Analyze(planets);
    }
}
