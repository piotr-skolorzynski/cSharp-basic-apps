namespace ToDoList.GameDataParser.UserInteraction;

public class ConsoleUserInteractor : IUserInteractor
{
    public void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadValidFilePath()
    {
        string fileName = string.Empty;
        bool isFilePathValid = false;

        do
        {
            Console.WriteLine("Please enter the file name you want to read:");
            fileName = Console.ReadLine();

            if (fileName is null)
            {
                Console.WriteLine("File name cannot be null.");
            }
            else if (fileName.Trim() == string.Empty)
            {
                Console.WriteLine("File name cannot be empty.");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine(
                    "The file does not exist. Please check the file name and try again."
                );
            }
            else
            {
                isFilePathValid = true;
            }
        } while (!isFilePathValid);

        return fileName;
    }
}
