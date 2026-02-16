namespace CookieCookbook.Recipes.Ingredients;

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
        var ingredientsWithGivenId = All.Where(ingredient => ingredient.Id == id);
        if (ingredientsWithGivenId.Count() > 1)
        {
            throw new InvalidOperationException($"More than one ingredient has ID equal to {id}.");
        }

        // if (All.Select(ingredient => ingredient.Id == id).Distinct().Count() != All.Count())
        // {
        //     throw new InvalidOperationException($"Some ingredients have duplicated IDs.");
        // }

        return ingredientsWithGivenId.FirstOrDefault();
    }
}
