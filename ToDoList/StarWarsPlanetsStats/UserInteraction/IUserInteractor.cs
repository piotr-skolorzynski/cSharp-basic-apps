namespace StarWarsPlanetsStats.UserInteraction;

public interface IUserInteractor
{
    void ShowMassage(string message);
    string? ReadFromUser();
}
