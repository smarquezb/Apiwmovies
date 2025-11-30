using API.P.Movies.DAL;
using API.P.Movies.DAL.Models;
using API.P.Movies.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace API.P.Movies.Repository
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
                .AnyAsync(c => c.Id == id); // esta linea significa que almenos un registro con ese Id existe, debuelve true o false

        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _context.Categories
            .AsNoTracking()
            .AnyAsync(c => c.Name == name);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            category.CreatedDate = DateTime.UtcNow;
            await _context.Categories.AddAsync(category);
            return await SaveAsync(); // Cuando inserto algo en base de datos debo llamar a este metodo SaveChangesAsync
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id); // primero busco que si exista la categoria

            if (category == null)
            {
                return false; //la categoria no existe
            }

            _context.Categories.Remove(category); // si existe la elimino
            return await SaveAsync();
            //sql Delete from Categories where Id = id
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)  //Ascending order, para ordenar descending usar .OrderByDescending
                .ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryAsync(int id) // palabras claves async y el await
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id); //lambda expression (Select * from Categories WHERE Id = id)
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
            // condicional ternario, si es mayor o igual a 0 devuelve true, sino false, el ? hace de entonces y el : hace de sino
        }
    }
}
