using ToDoList.GameDataParser.App;
using ToDoList.GameDataParser.DataAccess;
using ToDoList.GameDataParser.Logging;
using ToDoList.GameDataParser.UserInteraction;

var app = new GameDataParserApp(
    new ConsoleUserInteractor(),
    new GamesPrinter(new ConsoleUserInteractor()),
    new VideoGameDeserializer(new ConsoleUserInteractor()),
    new LocalFileReader()
);
var logger = new Logger("log.txt");

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(
        "Sorry! The application has experienced an unexpected error and will be closed."
    );
    logger.Log(ex);
}

Console.WriteLine("Press any key to close.");
Console.ReadKey();
