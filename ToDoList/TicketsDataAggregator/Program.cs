using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

const string TicketsFolder = @"D:\Repos\Tickets";

try
{
    var ticketsAggregator = new TicketsAggregator(TicketsFolder);
    ticketsAggregator.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured. Exception message: {ex.Message}");
}

Console.WriteLine("Press any key to close");
Console.ReadLine();

public class TicketsAggregator
{
    private readonly string _ticketsFolder;

    public TicketsAggregator(string ticketsFolder)
    {
        _ticketsFolder = ticketsFolder;
    }

    public void Run()
    {
        foreach (var filePath in Directory.GetFiles(_ticketsFolder, "*.pdf"))
        {
            using PdfDocument document = PdfDocument.Open(filePath);

            Page page = document.GetPage(1);

            string text = page.Text;

            var split = text.Split(
                new[] { "Title:", "Date:", "Time:", "Visit us:" },
                StringSplitOptions.None
            );
            //structure of pdf files
            //first element is a title of a ticket
            //each ticker is described in a sequence of 3 lines
            //so for loop will check try to process each of them
            for (int i = 1; i < split.Length - 3; i += 3)
            {
                var title = split[i];
                var date = split[i + 1];
                var time = split[i + 2];
            }
        }
    }
}
