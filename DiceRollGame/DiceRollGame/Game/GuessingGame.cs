using DiceRollGame.UserCommunication;

namespace DiceRollGame.Game;

public class GuessingGame
{
    private readonly Dice _dice;
    private const int MaxAttempts = 3;

    public GuessingGame(Dice dice)
    {
        _dice = dice;
    }

    public GameResult Play()
    {
        var diceRollResult = _dice.Roll();
        Console.WriteLine($"Dice rolled. Guess what number it shows in {MaxAttempts} attempts!");

        var attemptsLeft = MaxAttempts;
        while (attemptsLeft > 0)
        {
            var guess = ConsoleReader.ReadInteger("Enter a number between 1 and 6: ");
            if (guess == diceRollResult)
            {
                return GameResult.Victory;
            }
            --attemptsLeft;
        }
        return GameResult.Loss;
    }

    public static void PrintResult(GameResult result)
    {
        string message = result == GameResult.Victory ? "You won!" : "You lost! :()";

        Console.WriteLine(message);
    }
}
