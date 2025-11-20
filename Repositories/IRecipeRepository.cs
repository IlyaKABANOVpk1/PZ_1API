using Microsoft.Extensions.Hosting;
using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public interface IRecipeRepository : IRepository<Recipe>
    {

        bool RecipeExists(string name);

        Recipe? GetByName(string name);
        Recipe? GetByIdWithIngredients(int id);
    }
}
