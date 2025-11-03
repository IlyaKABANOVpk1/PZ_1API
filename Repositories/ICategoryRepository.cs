using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool CategoryExists(string name);
    }
}
