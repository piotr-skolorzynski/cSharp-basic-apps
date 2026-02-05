using System.Text.Json;
using CookieCookbook.Recipes;

var ingredientsRegistry = new IngredientsRegistry();

const FileFormat Format = FileFormat.Txt;

IStringsRepository stringsRespository =
    Format == FileFormat.Json ? new StringsJsonRepository() : new StringsTextualRepository();

const string FileName = "recipes";
var FileMetada = new FileMetada(FileName, Format);

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(stringsRespository, ingredientsRegistry),
    new RecipesConsoleUserInteraction(ingredientsRegistry)
);
cookiesRecipesApp.Run(FileMetada.ToPath());

public class FileMetada
{
    public string Name { get; }
    public FileFormat Format { get; }

    public FileMetada(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    public string ToPath() =>
        Format == FileFormat.Json
            ? $"{Name}{Format.AsFileExtension()}"
            : $"{Name}{Format.AsFileExtension()}";
}

public static class FileFormatExtensions
{
    public static string AsFileExtension(this FileFormat fileFormat) =>
        fileFormat == FileFormat.Json ? ".json" : ".txt";
}

public enum FileFormat
{
    Json,
    Txt,
}

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

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count() > 0)
        {
            var recipe = new Recipe(ingredients);
            allRecipies.Add(recipe);
            _recipesRepository.Write(filePath, allRecipies);
            _recipesUserInteraction.ShowMessage("Recipe added:");
            _recipesUserInteraction.ShowMessage(recipe.ToString());
        }
        else
        {
            _recipesUserInteraction.ShowMessage(
                "No ingredients have been selected. " + "Recipe will not be created."
            );
        }

        _recipesUserInteraction.Exit();
    }
}

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipies);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}

public interface IIngredientsRegistry
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetById(int id);
}

public class IngredientsRegistry : IIngredientsRegistry
{
    public IEnumerable<Ingredient> All { get; } =
        new List<Ingredient>
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

    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            {
                return ingredient;
            }
        }

        return null;
    }
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

public interface IRecipesRepository
{
    void Write(string filePath, List<Recipe> recipes);
    List<Recipe> Read(string filePath);
}

public class RecipesRepository : IRecipesRepository
{
    private const string Separator = ",";
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegistry _ingredientsRegistry;

    public RecipesRepository(
        IStringsRepository stringsRepository,
        IIngredientsRegistry ingredientsRegistry
    )
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegistry = ingredientsRegistry;
    }

    public void Write(string filePath, List<Recipe> recipes)
    {
        var recipesAsStrings = new List<string>();
        foreach (var recipe in recipes)
        {
            var allIds = new List<string>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id.ToString());
            }
            recipesAsStrings.Add(string.Join(Separator, allIds));
        }

        _stringsRepository.Write(filePath, recipesAsStrings);
    }

    public List<Recipe> Read(string filePath)
    {
        List<string> recipesFromFile = _stringsRepository.Read(filePath);
        var recipes = new List<Recipe>();

        foreach (var recipeIds in recipesFromFile)
        {
            var recipe = RecipeFromString(recipeIds);
            recipes.Add(recipe);
        }

        return recipes;
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        var ingredientIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();

        foreach (var ingredientId in ingredientIds)
        {
            var id = int.Parse(ingredientId);
            var ingredient = _ingredientsRegistry.GetById(id);
            ingredients.Add(ingredient);
        }

        return new Recipe(ingredients);
    }
}

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}

public class StringsTextualRepository : IStringsRepository
{
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            return fileContent.Split(Separator).ToList();
        }
        else
        {
            return new List<string>();
        }
    }

    public void Write(string filePath, List<string> strings)
    {
        var fileContent = string.Join(Separator, strings);
        File.WriteAllText(filePath, fileContent);
    }
}

public class StringsJsonRepository : IStringsRepository
{
    public List<string> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<string>>(fileContent);
        }
        else
        {
            return new List<string>();
        }
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(strings));
    }
}
