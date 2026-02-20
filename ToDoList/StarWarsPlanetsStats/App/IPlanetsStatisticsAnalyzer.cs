using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.App;

public interface IPlanetsStatisticsAnalyzer
{
    void Analyze(IEnumerable<Planet> planets);
}
