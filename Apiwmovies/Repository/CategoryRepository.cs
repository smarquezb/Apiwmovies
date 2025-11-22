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

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await _context.Categories
                  .AsNoTracking()
                  .AnyAsync(c => c.Id == id);
        }
        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _context.Categories
                  .AsNoTracking()
                  .AnyAsync(c => c.name == name);
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.CreatedDate = DateTime.UtcNow;

            var addedCategory = await _context.Categories.AddAsync(category);

            return await SaveAsync();

        }
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            category.ModifiedDate = DateTime.UtcNow;

            _context.Categories.Update(category);

            return await SaveAsync();

        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id); //primero consulto que si exista la categoria.




            if (category == null)
            {
                return false; // la categoría no existe
            }

            _context.Categories.Remove(category);

            return await SaveAsync();

        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id); //Lambda expressions

        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Solo estoy llamando el método desde la capa de Repository

            return _mapper.Map<ICollection<CategoryDto>>(categories);

            // Mapeo la lista de categorías a una lista de categorías DTO
        }
    }

}

/* Lista de categorias:
 
 *1-accion.
 *2-comedia
 *3-drama
 *4-ciencia ficcion
 *5-terror
 *6-romance
 *7-aventura
 *8-infantil
 