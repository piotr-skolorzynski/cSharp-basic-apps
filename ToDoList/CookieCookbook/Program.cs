var cookiesRecipesApp = new CookiesRecipesApp();
cookiesRecipesApp.Run();

public class CookiesRecipesApp
{
    private readonly RecipesRepository _recipesRepository;
    private readonly RecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(
        RecipesRepository recipesRepository,
        RecipesUserInteraction recipesUserInteraction
    )
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }

    public void Run()
    {
        var allRecipies = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipies);

        _recipesUserInteraction.PromptToCreateRecipe();

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count > 0)
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

internal class RecipesUserInteraction { }

internal class RecipesRepository { }


// public static class IngredientsGenerator
// {
//     public static List<Ingredient> GenerateIngredients()
//     {
//         return new List<Ingredient>
//         {
//             new()
//             {
//                 Id = 1,
//                 Name = "Wheat flour",
//                 PreparationInstruction = "Sieve. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 2,
//                 Name = "Coconut flour",
//                 PreparationInstruction = "Sieve. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 3,
//                 Name = "Butter",
//                 PreparationInstruction = "Melt on low heat. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 4,
//                 Name = "Chocolate",
//                 PreparationInstruction = "Melt in a water bath. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 5,
//                 Name = "Sugar",
//                 PreparationInstruction = "Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 6,
//                 Name = "Cardamom",
//                 PreparationInstruction = "Take half a teaspoon. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 7,
//                 Name = "Cinnamon",
//                 PreparationInstruction = "Take half a teaspoon. Add to other ingredients.",
//             },
//             new()
//             {
//                 Id = 8,
//                 Name = "Cocoa powder",
//                 PreparationInstruction = "Add to other ingredients.",
//             },
//         };
//     }
// }
