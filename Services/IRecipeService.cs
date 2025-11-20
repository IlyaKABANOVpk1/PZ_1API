using PZ_1API.Models.DTO;

namespace PZ_1API.Services
{
    public interface IRecipeService 
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto? GetById(int id);
        RecipeDto Create(RecipeCreateDto dto);
        RecipeDto Update(int id, RecipeUpdateDto dto);
        bool Delete(int id);
    }
}
