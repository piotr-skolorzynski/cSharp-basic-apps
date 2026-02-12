using System.Text.Json;
using ToDoList.GameDataParser;

var app = new GameDataParserApp();
var logger = new Logger("log.txt");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(
        "Sorry! The application has experienced an unexpected error and will be closed."
    );
    logger.Log(ex);
}

Console.WriteLine("Press any key to close.");
Console.ReadKey();

public class GameDataParserApp
{
    public void Run()
    {
        string fileName = ReadValidFilePathFromUser();
        string fileContents = File.ReadAllText(fileName);
        var videoGames = DeserializeVideoGamesFrom(fileName, fileContents);
        PrintGames(videoGames);
    }

    public string ReadValidFilePathFromUser()
    {
        string fileName = string.Empty;
        bool isFilePathValid = false;

        do
        {
            Console.WriteLine("Please enter the file name you want to read:");
            fileName = Console.ReadLine();

            if (fileName is null)
            {
                Console.WriteLine("File name cannot be null.");
            }
            else if (fileName.Trim() == string.Empty)
            {
                Console.WriteLine("File name cannot be empty.");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine(
                    "The file does not exist. Please check the file name and try again."
                );
            }
            else
            {
                isFilePathValid = true;
            }
        } while (!isFilePathValid);

        return fileName;
    }

    private static List<VideoGame> DeserializeVideoGamesFrom(string fileName, string fileContents)
    {
        try
        {
            return JsonSerializer.Deserialize<List<VideoGame>>(fileContents);
        }
        catch (JsonException ex)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"JSON in {fileName} file was not in a valid format. JSON body :");
            Console.WriteLine(fileContents);
            Console.ForegroundColor = originalColor;

            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
    }

    private static void PrintGames(List<VideoGame> videoGames)
    {
        if (videoGames.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Loaded video games:");

            foreach (var game in videoGames)
            {
                Console.WriteLine(game.ToString());
            }
        }
        else
        {
            Console.WriteLine("No video games found in the file.");
        }
    }
}

public class VideoGame
{
    public string Title { get; init; } = "";
    public int ReleaseYear { get; init; }
    public decimal Rating { get; init; }

    public override string ToString()
    {
        return $"{Title} released in {ReleaseYear}, rating: {Rating}";
    }
}
