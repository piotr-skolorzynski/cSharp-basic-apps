using ToDoList.GameDataParser.Model;

namespace ToDoList.GameDataParser.DataAccess;

public interface IVideoGameDeserializer
{
    List<VideoGame> Deserialize(string fileName, string fileContents);
}
