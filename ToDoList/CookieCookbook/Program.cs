using CookieCookbook.Recipes;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction(new IngredientsRegistry())
);
cookiesRecipesApp.Run("recipes.txt");

public class CookiesRecipesApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(
        IRecipesRepository recipesRepository,
        IRecipesUserInteraction recipesUserInteraction
    )
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }

    public void Run(string filePath)
    {
        var allRecipies = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipies);

        _recipesUserInteraction.PromptToCreateRecipe();

        // var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        // if (ingredients.Count > 0)
        // {
        //     var recipe = new Recipe(ingredients);
        //     allRecipies.Add(recipe);
        //     _recipesRepository.Write(filePath, allRecipies);
        //     _recipesUserInteraction.ShowMessage("Recipe added:");
        //     _recipesUserInteraction.ShowMessage(recipe.ToString());
        // }
        // else
        // {
        //     _recipesUserInteraction.ShowMessage(
        //         "No ingredients have been selected. " + "Recipe will not be created."
        //     );
        // }

        _recipesUserInteraction.Exit();
    }
}

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipies);
    void PromptToCreateRecipe();
}

public class IngredientsRegistry
{
    public IEnumerable<Ingredient> All = new List<Ingredient>
    {
        new WheatFlour(),
        new SpeltFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder(),
    };
}

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

            var counter = 1;
            foreach (var recipe in allRecipies)
            {
                Console.WriteLine($"*******{counter}*******");
                Console.WriteLine(recipe.ToString());
                Console.WriteLine();
                ++counter;
            }
        }
        else
        {
            Console.WriteLine("No recipes found.");
        }
    }

    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe! " + "Available ingredients are:");

        foreach (var ingredient in _ingredientsRegistry.All)
        {
            Console.WriteLine(ingredient);
        }
    }
}

public interface IRecipesRepository
{
    void Write(string filePath, List<Recipe> recipes);
    List<Recipe> Read(string filePath);
}

public class RecipesRepository : IRecipesRepository
{
    public void Write(string filePath, List<Recipe> recipes)
    {
        Console.WriteLine($"Writing recipes to file: {filePath}");
    }

    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>
        {
            new(new List<Ingredient> { new WheatFlour(), new Butter(), new Sugar() }),
            new(new List<Ingredient> { new Cardamom(), new Cinnamon() }),
        };
    }
}
