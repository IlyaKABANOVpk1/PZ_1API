using PZ_1API.Models.DTO;

namespace PZ_1API.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto? GetById(int id);
        CategoryDto Create(CategoryDto dto);
        CategoryDto Update(CategoryDto dto);
        bool Delete(int id);
    }
}
