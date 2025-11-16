using Apiwmovies.DAL.Models;
using Apiwmovies.DAL.Models.Dtos;
using Apiwmovies.Repository;
using Apiwmovies.Services.IServices;
using AutoMapper;

namespace Apiwmovies.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)

        {

            _categoryRepository = categoryRepository;
            _mapper = mapper;

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

        public Task<bool> DeleteCategoryAsync(int id)

        {

            throw new NotImplementedException();

        }

        public Task<Category> GetCategoryAsync(int id)

        {

            throw new NotImplementedException();

        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()

        {

            var categories = _categoryRepository.GetCategoriesAsync();

            //Solo estoy llamando el método desde la capa de Repository
            


           return _mapper.Map<ICollection<CategoryDto>>(categories);

        }

        public Task<bool> UpdateCategoryAsync(Category category)

        {

            throw new NotImplementedException();

        }
    }

    
}
