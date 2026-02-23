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

System.Console.WriteLine("Press any key to close");
Console.ReadKey();

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
        }
    }
}
