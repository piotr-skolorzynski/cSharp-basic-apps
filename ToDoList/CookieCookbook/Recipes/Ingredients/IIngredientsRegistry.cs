namespace CookieCookbook.Recipes.Ingredients;

public interface IIngredientsRegistry
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetById(int id);
}
