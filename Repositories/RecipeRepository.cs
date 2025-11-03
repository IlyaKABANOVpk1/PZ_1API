using Microsoft.EntityFrameworkCore;
using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {

        private readonly CookbookDbContext _context;
        public RecipeRepository(CookbookDbContext context)
        {
            _context = context;
        }
        public Recipe Create(Recipe entity)
        {
             _context.Recipes.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var recipe = GetById(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        

        public bool Exists(int id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetAll()
        {
            return _context.Recipes
                .Include(i => i.Id)
                .ToList();
        }

        public Recipe GetById(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            return recipe;
        }

        public bool RecipeExists(string name)
        {
            return _context.Recipes.Any(c => c.Name == name);
        }

        public Recipe Update(Recipe entity)
        {
            _context.Recipes.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
