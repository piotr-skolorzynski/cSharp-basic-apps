using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.App;

public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IngredientsRegistry _ingredientsRegistry;

    public RecipesConsoleUserInteraction(IngredientsRegistry ingredientsRegistry)
    {
        _ingredientsRegistry = ingredientsRegistry;
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void Exit()
    {
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipies)
    {
        if (allRecipies.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);

            var allRecipesAsStrings = allRecipies.Select(
                (recipe, index) =>
                    $@"*****{index + 1}***** 
{recipe}"
            );

            Console.WriteLine(string.Join(Environment.NewLine, allRecipesAsStrings));
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("No recipes found.");
        }
    }

    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe! " + "Available ingredients are:");

        Console.WriteLine(string.Join(Environment.NewLine, _ingredientsRegistry.All));
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        bool shallStop = false;
        var ingredients = new List<Ingredient>();
        while (!shallStop)
        {
            Console.WriteLine("Add an ingredient by its ID, " + "or type anything else to finish");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegistry.GetById(id);

                if (selectedIngredient is not null)
                {
                    ingredients.Add(selectedIngredient);
                }
            }
            else
            {
                shallStop = true;
            }
        }

        return ingredients;
    }
}
