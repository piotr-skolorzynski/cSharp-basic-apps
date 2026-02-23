using System.Globalization;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TicketsDataAggregator.TicketsAggregation;

public class TicketsAggregator
{
    private readonly string _ticketsFolder;
    private readonly Dictionary<string, CultureInfo> _domainToCultureMapping = new()
    {
        [".com"] = new CultureInfo("en-US"),
        [".fr"] = new CultureInfo("fr-FR"),
        [".jp"] = new CultureInfo("ja-JP"),
    };

    public TicketsAggregator(string ticketsFolder)
    {
        _ticketsFolder = ticketsFolder;
    }

    public void Run()
    {
        var stringBuilder = new StringBuilder();

        foreach (var filePath in Directory.GetFiles(_ticketsFolder, "*.pdf"))
        {
            using PdfDocument document = PdfDocument.Open(filePath);

            Page page = document.GetPage(1);

            var lines = ProcessPage(page);
            stringBuilder.AppendLine(string.Join(Environment.NewLine, lines));

            SaveTicketsData(stringBuilder);
        }
    }

    private IEnumerable<string> ProcessPage(Page page)
    {
        var split = page.Text.Split(
            new[] { "Title:", "Date:", "Time:", "Visit us:" },
            StringSplitOptions.None
        );

        var domain = ExtractDomain(split.Last());
        var ticketCulture = _domainToCultureMapping[domain];

        for (int i = 1; i < split.Length - 3; i += 3)
        {
            yield return BuildTicketData(split, i, ticketCulture);
        }
    }

    private static string BuildTicketData(string[] split, int i, CultureInfo ticketCulture)
    {
        //structure of pdf files
        //first element is a title of a ticket
        //each ticker is described in a sequence of 3 lines
        //so for loop will check try to process each of them
        var title = split[i];
        var dateAsString = split[i + 1];
        var timeAsString = split[i + 2];

        var date = DateOnly.Parse(dateAsString, ticketCulture);
        var time = TimeOnly.Parse(timeAsString, ticketCulture);

        var dateAsStringInvariant = date.ToString(CultureInfo.InvariantCulture);
        var timeAsStringInvariant = time.ToString(CultureInfo.InvariantCulture);

        return $"{title, -40}|{dateAsStringInvariant}|{timeAsStringInvariant}";
    }

    private void SaveTicketsData(StringBuilder stringBuilder)
    {
        var resultPath = Path.Combine(_ticketsFolder, "aggregatorTickets.txt");
        File.WriteAllText(resultPath, stringBuilder.ToString());
        Console.WriteLine("Results save to " + resultPath);
    }

    private static string ExtractDomain(string webAddress)
    {
        var lastDotIndex = webAddress.LastIndexOf('.');
        return webAddress.Substring(lastDotIndex);
    }
}
