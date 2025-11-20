using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

       
        public List<IngredientDto> Ingredients { get; set; }
    }
}
