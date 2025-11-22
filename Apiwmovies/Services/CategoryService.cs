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

        public  async Task<bool> CategoryExistsByIdAsync(int id)

        {

            throw new NotImplementedException();

        }

        public async Task<bool> CategoryExistsByNameAsync(string name)

        {

            throw new NotImplementedException();

        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)

        {

            //Validar si la categoría ya existe

            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name);

``            if (categoryExists)

            {

                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDto.Name}'");

            }

``

           //Mapear el DTO a la entidad

         var category = _mapper.Map<Category>(categoryCreateDto);

``

            //Crear la categoría en el repositorio

         var categoryCreated= await _categoryRepository.CreateCategoryAsync(category);

        }
        if (!categoryCreated)
{
    throw new Exception("Ocurrió un error al crear la categoría.");
    }

          //Mapear la entidad creada a DTO
          return _mapper.Map<CategoryDto>(category);

        public async Task<bool> CreateCategoryAsync(Category category)

        {

            category.CreatedDate = DateTime.UtcNow;

            await _context.Categories.AddAsync(category);

            return await SaveAsync();

        }
        public async Task<bool> DeleteCategoryAsync(int id)

        {

            throw new NotImplementedException();

        }

        public async Task<Category> GetCategoryAsync(int id)

        {

            // Obtener la categoría del repositorio

            var category = await _categoryRepository.GetCategoryAsync(id);

``

           // Mapear toda la colección de una vez

           return _mapper.Map<CategoryDto>(category);

        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()

        {

            var categories = _categoryRepository.GetCategoriesAsync();

            //Solo estoy llamando el método desde la capa de Repository
            


           return _mapper.Map<ICollection<CategoryDto>>(categories);

        }

        public async  Task<bool> UpdateCategoryAsync(Category category)

        {

            throw new NotImplementedException();

        }
    }

    
}
