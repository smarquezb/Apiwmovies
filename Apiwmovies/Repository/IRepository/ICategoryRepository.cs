using API.P.Movies.DAL.Models;

namespace API.P.Movies.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync(); //retorna lista de categorias
        Task<Category> GetCategoryAsync(int id); //Me retorna una categoria por su Id
        Task<bool> CategoryExistsByIdAsync(int id); //Me dice si una categoria existe por su Id
        Task<bool> CategoryExistsByNameAsync(string name); //Me dice si una categoria existe por su nombre
        Task<bool> CreateCategoryAsync(Category category); //Me crea una categoria
        Task<bool> UpdateCategoryAsync(Category category); //Me actualiza una categoria, puedo actualizar el nombre y la fecha de actualizacion
        Task<bool> DeleteCategoryAsync(int id); //Me elimina una categoria
        
        //  son las 7 firmas de los metodos
    }
}

