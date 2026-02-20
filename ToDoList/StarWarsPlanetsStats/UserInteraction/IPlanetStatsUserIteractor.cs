using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.UserInteraction;

public interface IPlanetStatsUserIteractor
{
    void Show(IEnumerable<Planet> planets);
    string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThaCanBeChosen);
    void ShowMessage(string message);
}
