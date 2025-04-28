Console.WriteLine("Hello, World!");
Console.WriteLine("[S]ee all TODOs");
Console.WriteLine("[A]dd a TODO");
Console.WriteLine("[R]emove a TODO");
Console.WriteLine("[E]xit");

string userChoice = Console.ReadLine().Trim().ToLower();

switch(userChoice)
{
    case "s":
        PrintSelectedOption("See all TODOs");
        break;
    case "a":
        PrintSelectedOption("Add a TODO");
        break;
    case "r":
        PrintSelectedOption("Remove a TODO");
        break;
    case "e":
        PrintSelectedOption("Exit");
        break;
    default:
        PrintSelectedOption("Invalid choice");
        break;
}

void PrintSelectedOption(string option)
{
    Console.WriteLine(option);
}

Console.ReadKey();