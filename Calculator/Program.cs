Console.WriteLine("Hello!");
Console.WriteLine("Input the first number:");
var firstNumber = int.Parse(Console.ReadLine());
Console.WriteLine("Input the second number:");
var secondNumber = int.Parse(Console.ReadLine());

Console.WriteLine("What do you want to do with those numbers?");
Console.WriteLine("[A]dd");
Console.WriteLine("[S]ubtract");
Console.WriteLine("[M]ultiply");
var userChoice = Console.ReadLine().Trim().ToLower();

if(isAvailableOperation(userChoice))
{
    calculate(firstNumber, secondNumber, userChoice);
}
else
{
    Console.WriteLine("Invalid option");
}

Console.WriteLine("Press any key to close");
Console.ReadKey();


void calculate(int a, int b, string choice)
{
    if (choice == "a")
    {
        Console.WriteLine(a + " + " + b + " = " + (a + b));
    }
    else if (choice == "s")
    {
        Console.WriteLine(a + " - " + b + " = " + (a - b));
    }
    else if (choice == "m")
    {
        Console.WriteLine(a + " * " + b + " = " + (a * b));
    }

}

bool isAvailableOperation(string choice)
{
    return choice == "a" || choice == "s" || choice == "m";
}
