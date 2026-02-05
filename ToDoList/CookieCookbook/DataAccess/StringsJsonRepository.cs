using System.Text.Json;

namespace CookieCookbook.DataAccess;

public class StringsJsonRepository : StringsRepository
{
    protected override string StringsToText(List<string> strings)
    {
        return JsonSerializer.Serialize(strings);
    }

    protected override List<string> TextToStrings(string fileContent)
    {
        return JsonSerializer.Deserialize<List<string>>(fileContent);
    }
}
