using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public interface IIngridientRepository : IRepository<Ingredient>
    {
        bool IngridientExists(string name);

    }
}
