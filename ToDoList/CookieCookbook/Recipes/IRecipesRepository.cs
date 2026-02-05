namespace CookieCookbook.Recipes;

public interface IRecipesRepository
{
    void Write(string filePath, List<Recipe> recipes);
    List<Recipe> Read(string filePath);
}
