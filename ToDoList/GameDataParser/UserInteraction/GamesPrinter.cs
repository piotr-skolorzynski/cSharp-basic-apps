using ToDoList.GameDataParser.Model;

namespace ToDoList.GameDataParser.UserInteraction;

public class GamesPrinter : IGamesPrinter
{
    private readonly IUserInteractor _userInteractor;

    public GamesPrinter(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public void Print(List<VideoGame> videoGames)
    {
        if (videoGames.Count > 0)
        {
            _userInteractor.PrintMessage(Environment.NewLine + "Loaded video games:");

            foreach (var game in videoGames)
            {
                _userInteractor.PrintMessage(game.ToString());
            }
        }
        else
        {
            _userInteractor.PrintMessage("No video games found in the file.");
        }
    }
}
