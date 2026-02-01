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

void SeeAllTodos()
{
    if (todos.Count == 0)
    {
        ShowNoTodosMessage();
        return;
    }

    Console.WriteLine("Your TODOs:");
    for (int i = 0; i < todos.Count; i++)
    {
        string? todo = todos[i];
        Console.WriteLine($"{i + 1}. {todo}");
    }
}

void AddTodo()
{
    string description;
    do
    {
        Console.WriteLine("Enter the TODO description:");
        description = Console.ReadLine();
    } while (!IdDescriptionValid(description));

    todos.Add(description);
}

void RemoveTodo()
{
    if (todos.Count == 0)
    {
        ShowNoTodosMessage();
        return;
    }

    int index;
    do
    {
        Console.WriteLine("Selected the index of the TODO to remove:");
        SeeAllTodos();
    } while (!TryReadIndex(out index));

    RemoveTodoAtIndex(index - 1);
}

void RemoveTodoAtIndex(int index)
{
    var todoToRemove = todos[index];
    todos.RemoveAt(index);
    Console.WriteLine($"Removed TODO: {todoToRemove}");
}

void PrintSelectedOption(string option)
{
    Console.WriteLine(option);
}

void ShowNoTodosMessage()
{
    Console.WriteLine("No TODOs have been added yet.");
}

bool IdDescriptionValid(string description)
{
    if (description == "")
    {
        Console.WriteLine("Description cannot be empty.");
        return false;
    }
    else if (todos.Contains(description))
    {
        Console.WriteLine("This description must be unique.");
        return false;
    }

    return true;
}

bool TryReadIndex(out int index)
{
    var userInput = Console.ReadLine();

    if (userInput == "")
    {
        index = 0;
        Console.WriteLine("Selected index cannot be empty.");
        return false;
    }

    if (int.TryParse(userInput, out index) && index >= 1 && index <= todos.Count)
    {
        return true;
    }

    Console.WriteLine("The given index is not valid.");
    return false;
}
