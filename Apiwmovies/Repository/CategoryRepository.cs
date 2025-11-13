using Apiwmovies.DAL;
using Apiwmovies.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace Apiwmovies.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();

        }
        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        public Task<bool> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id); //Lambda expressions

        }

        public Task<ICollection<Category>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }

}

