using Apiwmovies.DAL.Models;

namespace Apiwmovies.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync(); //Me retorna UNA LISTA DE CATEGORIAS
        Task<Category> GetCategoryAsync(int id); //Me retorna UNA CATEGORIA POR ID
        Task<bool> CategoryExistsByIdAsync(int id); //Me dice si existe una categoría por ID
        Task<bool> CategoryExistsByNameAsync(string name); //Me dice si existe una categoría por Nombre
        Task<bool> CreateCategoryAsync(Category category); //Me crea una categoría
        Task<bool> UpdateCategoryAsync(Category category); //Me crea una categoría — puedo actualizar el nombre y la fecha de actualización
        Task<bool> DeleteCategoryAsync(int id); //Me elimina una categoría

    }
}
