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
        //short version
        // var recipesAsStrings = recipes.Select(recipe =>
        //     string.Join(
        //         Separator,
        //         recipe.Ingredients.Select(ingredient => ingredient.Id.ToString())
        //     )
        // );

        var recipesAsStrings = recipes
            .Select(recipe =>
            {
                var ids = recipe.Ingredients.Select(ingredient => ingredient.Id.ToString());

                return string.Join(Separator, ids);
            })
            .ToList();

        _stringsRepository.Write(filePath, recipesAsStrings.ToList());
    }

    public List<Recipe> Read(string filePath)
    {
        // return _stringsRepository.Read(filePath).Select(recipeFromFile =>
        //     RecipeFromString(recipeFromFile)
        // );

        //shorter version
        return _stringsRepository.Read(filePath).Select(RecipeFromString).ToList();
    }

    private Recipe RecipeFromString(string recipeFromFile)
    {
        var ingredients = recipeFromFile
            .Split(Separator)
            .Select(int.Parse) //shorthand to replace .Select(id => int.Parse(id))
            .Select(_ingredientsRegistry.GetById); //.Select(id => _ingredientsRegistry.GetById)

        return new Recipe(ingredients);
    }
}
