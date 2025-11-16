using Apiwmovies.DAL.Models;

namespace Apiwmovies.Repository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
    }
}