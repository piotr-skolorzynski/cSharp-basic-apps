using System.Text.Json;
using ToDoList.GameDataParser.Model;
using ToDoList.GameDataParser.UserInteraction;

namespace ToDoList.GameDataParser.DataAccess;

public class VideoGameDeserializer : IVideoGameDeserializer
{
    private readonly IUserInteractor _userInteractor;

    public VideoGameDeserializer(IUserInteractor userInteractor)
    {
        _userInteractor = userInteractor;
    }

    public List<VideoGame> Deserialize(string fileName, string fileContents)
    {
        try
        {
            var result = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);
            return result ?? new List<VideoGame>();
        }
        catch (JsonException ex)
        {
            _userInteractor.PrintError(
                $"JSON in {fileName} file was not in a valid format. JSON body :"
            );
            _userInteractor.PrintError(fileContents);

            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
    }
}
