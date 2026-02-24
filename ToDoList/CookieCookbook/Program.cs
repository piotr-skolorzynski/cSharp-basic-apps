using CookieCookbook.App;
using CookieCookbook.DataAccess;
using CookieCookbook.FileAccess;
using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

try
{
    const FileFormat Format = FileFormat.Txt;

    IStringsRepository stringsRespository =
        Format == FileFormat.Json ? new StringsJsonRepository() : new StringsTextualRepository();

    const string FileName = "recipes";
    var FileMetada = new FileMetada(FileName, Format);

    var ingredientsRegistry = new IngredientsRegistry();
    var cookiesRecipesApp = new CookiesRecipesApp(
        new RecipesRepository(stringsRespository, ingredientsRegistry),
        new RecipesConsoleUserInteraction(ingredientsRegistry)
    );
    cookiesRecipesApp.Run(FileMetada.ToPath());
}
catch (Exception ex)
{
    Console.WriteLine(
        $"Sorry! The application experienced an unexpected error and will have to be closed. The error message: {ex.Message}"
    );
    Console.WriteLine("Press any key to close.");
    Console.ReadKey();
}

// var order = 40;
// var result = CheckedFibonacciExercise.GetFibonacci(order);
// foreach (var item in result)
// {
//     System.Console.WriteLine(item);
// }

// public static class CheckedFibonacciExercise
// {
//     public static IEnumerable<int> GetFibonacci(int n)
//     {
//         checked
//         {
//             int a = -1;
//             int b = 1;
//             for (int i = 0; i < n; i++)
//             {
//                 int sum = a + b;
//                 yield return sum;
//                 a = b;
//                 b = sum;
//             }
//         }
//     }
// }
