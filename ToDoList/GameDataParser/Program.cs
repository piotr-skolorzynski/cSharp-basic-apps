using System.Text.Json;

bool isFileRead = false;
string fileContents = string.Empty;

do
{
    try
    {
        Console.WriteLine("Please enter the file name you want to read:");

        var fileName = Console.ReadLine();

        fileContents = File.ReadAllText(fileName);

        isFileRead = true;
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine("File name cannot be empty. Please provide a valid file name.");
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine(
            "The specified file was not found. Please check the file name and try again."
        );
    }
} while (!isFileRead);

var videoGames = JsonSerializer.Deserialize<List<VideoGame>>(fileContents);

if (videoGames.Count > 0)
{
    System.Console.WriteLine();
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

Console.ReadKey();

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
