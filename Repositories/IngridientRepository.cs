using Microsoft.EntityFrameworkCore;
using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public class IngridientRepository : IIngridientRepository
    {
        private readonly CookbookDbContext _context;
        public IngridientRepository(CookbookDbContext context)
        {
            _context = context;
        }

        public Ingredient Create(Ingredient entity) 
        {
            _context.Ingredients.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var ingr = _context.Ingredients.FirstOrDefault(i => i.Id == id);
            if (ingr != null)
            {
                _context.Ingredients.Remove(ingr);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exists(int id)
        {
            return _context.Ingredients.Any(i => i.Id == id);
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients
                .Include(i => i.Id)
                .ToList();
        }

        public Ingredient GetById(int id)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(x => x.Id == id);
            return ingredient;
        }

        public bool IngridientExists(string name)
        {
            return _context.Ingredients.Any(i => i.Name == name);
        }

        public Ingredient Update(Ingredient entity)
        {
            _context.Ingredients.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<Ingredient> GetByIds(List<int> ids)
        {
            // SELECT * FROM Ingredients WHERE Id IN (1, 2, 3...)
            return _context.Ingredients
                .Where(i => ids.Contains(i.Id))
                .ToList();
        }
    }
}
