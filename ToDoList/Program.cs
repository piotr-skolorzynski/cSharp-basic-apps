var todos = new List<string>();

Console.WriteLine("Hello");

bool shallExit = false;
while (!shallExit)
{
    System.Console.WriteLine("====================");
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
            SeeAllTodos();
            break;
        case "a":
            PrintSelectedOption("Add a TODO");
            AddTodo();
            break;
        case "r":
            PrintSelectedOption("Remove a TODO");
            RemoveTodo();
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

Console.ReadKey();

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

void SeeAllTodos()
{
    if (todos.Count == 0)
    {
        Console.WriteLine("No TODOs found.");
        return;
    }

    Console.WriteLine("Your TODOs:");
    for (int i = 0; i < todos.Count; i++)
    {
        string? todo = todos[i];
        Console.WriteLine($"{i + 1}. {todo}");
    }
}

void RemoveTodo()
{
    if (todos.Count == 0)
    {
        Console.WriteLine("No TODOs to remove.");
        return;
    }

    bool isIndexValid = false;
    while (!isIndexValid)
    {
        Console.WriteLine("Select the number of the TODO to remove:");
        SeeAllTodos();

        var userInput = Console.ReadLine();

        if (userInput == "")
        {
            Console.WriteLine("Selected index cannot be empty.");
            continue;
        }

        if (int.TryParse(userInput, out int index) && index >= 1 && index <= todos.Count)
        {
            isIndexValid = true;
            var indexOfTodo = index - 1;
            var todoToRemove = todos[indexOfTodo];
            todos.RemoveAt(indexOfTodo);
            Console.WriteLine($"Removed TODO: {todoToRemove}");
        }
        else
        {
            Console.WriteLine("The given index is not valid.");
        }
    }
}
