using StarWarsPlanetsStats.Models;

namespace StarWarsPlanetsStats.UserInteraction;

public class PlanetsStatsUserInteractor : IPlanetStatsUserIteractor
{
    private readonly IUserInteractor _userInteractor;

    public PlanetsStatsUserInteractor(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public string? ChooseStatisticsToBeShown(IEnumerable<string> propertiesThaCanBeChosen)
    {
        _userInteractor.ShowMassage(Environment.NewLine);
        _userInteractor.ShowMassage("The statistics of which property would you like to see?");
        _userInteractor.ShowMassage(string.Join(Environment.NewLine, propertiesThaCanBeChosen));

        return _userInteractor.ReadFromUser();
    }

    public void Show(IEnumerable<Planet> planets)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
