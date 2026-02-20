using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.DataAccess;

public interface IPlanetsReader
{
    Task<IEnumerable<Planet>> Read();
}
