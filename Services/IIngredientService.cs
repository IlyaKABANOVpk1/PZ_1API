using PZ_1API.Models.DTO;

namespace PZ_1API.Services
{
    public interface IIngredientService 
    {
        IEnumerable<IngredientDto> GetAll();
        IngredientDto? GetById(int id);
        IngredientDto Create(IngredientDto dto);
        IngredientDto Update(IngredientDto dto);
        bool Delete(int id);
    }
}
