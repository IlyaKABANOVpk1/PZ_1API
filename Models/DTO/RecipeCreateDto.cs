namespace PZ_1API.Models.DTO
{
    public class RecipeCreateDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
      
        public List<int> IngredientIds { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
