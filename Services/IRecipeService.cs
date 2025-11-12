using PZ_1API.Models.DTO;

namespace PZ_1API.Services
{
    public interface IRecipeService 
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto? GetById(int id);
        RecipeDto Create(RecipeDto dto);
        RecipeDto Update(RecipeDto dto);
        bool Delete(int id);
    }
}
