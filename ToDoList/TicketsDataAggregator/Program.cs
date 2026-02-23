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
        using (PdfDocument document = PdfDocument.Open(_ticketsFolder + @"\Tickets1.pdf"))
        {
            // int pageCount = document.NumberOfPages;

            // Page number starts from 1, not 0.
            Page page = document.GetPage(1);

            // decimal widthInPoints = page.Width;
            // decimal heightInPoints = page.Height;

            string text = page.Text;
        }
    }
}

// using (PdfDocument document = PdfDocument.Open(@"C:\Documents\document.pdf"))
// {
//     foreach (Page page in document.GetPages())
//     {
//         string text = ContentOrderTextExtractor.GetText(page);
//         IEnumerable<Word> words = page.GetWords(NearestNeighbourWordExtractor.Instance);
//     }
// }
