using CookieCookbook.App;
using CookieCookbook.DataAccess;
using CookieCookbook.FileAccess;
using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

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
