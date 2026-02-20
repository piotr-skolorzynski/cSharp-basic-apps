namespace StarWarsPlanetsStats.UserInteraction;

public class ConsoleUserInteractor : IUserInteractor
{
    public string? ReadFromUser()
    {
        return Console.ReadLine();
    }

    public void ShowMassage(string message)
    {
        Console.WriteLine(message);
    }
}
