using PZ_1API.Models;
using Microsoft.EntityFrameworkCore;
namespace PZ_1API.Models
{
    public class CookbookDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base(options) { }
    }
}
