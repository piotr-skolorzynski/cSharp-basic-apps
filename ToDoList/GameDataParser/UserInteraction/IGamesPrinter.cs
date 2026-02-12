using ToDoList.GameDataParser.Model;

namespace ToDoList.GameDataParser.UserInteraction;

public interface IGamesPrinter
{
    void Print(List<VideoGame> videoGames);
}
