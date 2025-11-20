using System.ComponentModel.DataAnnotations;

namespace PZ_1API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    }
}
