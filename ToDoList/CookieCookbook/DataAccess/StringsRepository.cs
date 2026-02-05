namespace CookieCookbook.DataAccess;

public abstract class StringsRepository : IStringsRepository
{
    public List<string> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            return TextToStrings(fileContent);
        }
        else
        {
            return new List<string>();
        }
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, StringsToText(strings));
    }

    protected abstract List<string> TextToStrings(string fileContent);

    protected abstract string StringsToText(List<string> strings);
}
