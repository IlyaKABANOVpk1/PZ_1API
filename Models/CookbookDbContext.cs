using PZ_1API.Models;
using Microsoft.EntityFrameworkCore;
namespace PZ_1API.Models
{
    public class CookbookDbContext : DbContext
    {
        

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base(options) { }
    }
}
