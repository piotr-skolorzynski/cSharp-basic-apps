using System.Globalization;
using System.Text;
using TicketsDataAggregator.FileAccess;
using TicketsDataAggregator.TicketExtensions;

namespace TicketsDataAggregator.TicketsAggregation;

public class TicketsAggregator
{
    private readonly IFileWriter _fileWriter;
    private readonly IDocumentsReader _documentsReader;
    private readonly string _ticketsFolder;
    private readonly Dictionary<string, CultureInfo> _domainToCultureMapping = new()
    {
        [".com"] = new CultureInfo("en-US"),
        [".fr"] = new CultureInfo("fr-FR"),
        [".jp"] = new CultureInfo("ja-JP"),
    };

    public TicketsAggregator(
        string ticketsFolder,
        IFileWriter fileWriter,
        IDocumentsReader documentsReader
    )
    {
        _ticketsFolder = ticketsFolder;
        _fileWriter = fileWriter;
        _documentsReader = documentsReader;
    }

    public void Run()
    {
        var stringBuilder = new StringBuilder();

        var ticketDocuments = _documentsReader.Read(_ticketsFolder);

        foreach (var document in ticketDocuments)
        {
            var lines = ProcessDocument(document);
            stringBuilder.AppendLine(string.Join(Environment.NewLine, lines));
        }

        _fileWriter.Write(stringBuilder.ToString(), _ticketsFolder, "aggregatedTickets.txt");
    }

    private IEnumerable<string> ProcessDocument(string document)
    {
        var split = document.Split(
            new[] { "Title:", "Date:", "Time:", "Visit us:" },
            StringSplitOptions.None
        );

        var domain = split.Last().ExtractDomain();
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
}
