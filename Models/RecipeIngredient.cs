using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models
{
    public class RecipeIngredient
    {
            [Key]
            public int Id { get; set; }
            public int RecipeId { get; set; }
            public Recipe Recipe { get; set; }
            public int IngredientId { get; set; }
            public Ingredient Ingredient { get; set; }
            public string Quantity { get; set; }
        
    }
}
