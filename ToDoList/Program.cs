var todos = new List<string>();

Console.WriteLine("Hello");

bool shallExit = false;
while (!shallExit)
{
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("[S]ee all TODOs");
    Console.WriteLine("[A]dd a TODO");
    Console.WriteLine("[R]emove a TODO");
    Console.WriteLine("[E]xit");

    string userChoice = Console.ReadLine().Trim().ToLower();

    switch (userChoice)
    {
        case "s":
            PrintSelectedOption("See all TODOs");
            break;
        case "a":
            PrintSelectedOption("Add a TODO");
            AddTodo();
            break;
        case "r":
            PrintSelectedOption("Remove a TODO");
            break;
        case "e":
            PrintSelectedOption("Exit");
            shallExit = true;
            break;
        default:
            PrintSelectedOption("Invalid choice");
            break;
    }
}

void PrintSelectedOption(string option)
{
    Console.WriteLine(option);
}

void AddTodo()
{
    bool isValidDescription = false;
    while (!isValidDescription)
    {
        Console.WriteLine("Enter the TODO description:");
        var description = Console.ReadLine();
        if (description == "")
        {
            Console.WriteLine("Description cannot be empty.");
        }
        else if (todos.Contains(description))
        {
            Console.WriteLine("This description must be unique.");
        }
        else
        {
            isValidDescription = true;
            todos.Add(description);
        }
    }
}

Console.ReadKey();
