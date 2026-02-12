namespace ToDoList.GameDataParser.Model;

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
