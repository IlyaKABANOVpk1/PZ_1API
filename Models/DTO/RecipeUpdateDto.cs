namespace PZ_1API.Models.DTO
{
    public class RecipeUpdateDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public List<int> IngredientIds { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
