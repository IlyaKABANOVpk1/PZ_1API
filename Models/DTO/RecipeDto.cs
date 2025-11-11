using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models.DTO
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
    }
}
