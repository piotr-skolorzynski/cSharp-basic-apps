using CookieCookbook.DataAccess;
using CookieCookbook.Recipes.Ingredients;

namespace CookieCookbook.Recipes;

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
