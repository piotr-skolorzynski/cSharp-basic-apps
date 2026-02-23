using TicketsDataAggregator.TicketsAggregation;

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
