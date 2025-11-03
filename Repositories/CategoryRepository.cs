using Microsoft.EntityFrameworkCore;
using PZ_1API.Models;

namespace PZ_1API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly CookbookDbContext _context;
        public CategoryRepository(CookbookDbContext context)
        {
            _context = context;
        }
        public bool CategoryExists(string name)
        {
            return _context.Categories.Any(c => c.Name == name);
        }

        public Category Create(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Exists(int id)
        {
            return _context.Categories.Any(i => i.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories
                .Include(i => i.Id)
                .ToList();
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return category;
        }

        public Category Update(Category entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
