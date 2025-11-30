using API.P.Movies.DAL.Models;
using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Repository.IRepository;
using API.P.Movies.Services.IServices;
using AutoMapper;

namespace API.P.Movies.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto categoryCreateDto)
        {
            //Validar si la categoria ya existe
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name);

            if (categoryExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDto.Name}'");
            }

            //Mapear el DTO a la entidad Category
            var category = _mapper.Map<Category>(categoryCreateDto);


            //Crear la categoria en el repositorio (base de datos)
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Ocurrió un error al crear la categoría.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync(); //Solo estoy llamando al metodo desde la capa Repository

            return _mapper.Map<ICollection<CategoryDto>>(categories); //Mapeo la lista de categorias a lista de CategoryDto, para no exponer mi modelo


        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            // obtener la categoria por id desde el repositorio
            var category = await _categoryRepository.GetCategoryAsync(id);

            // mappear toda la coleccion de una vez 
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryCreateUpdateDto dto, int id)
        {
            //verificar si la categoria existe
            var existingCategory = await _categoryRepository.GetCategoryAsync(id);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"No se encontró la categoría con ID {id}");
            }

            //verificar si el nuevo nombre ya existe en otra categoria
            var categoryExistsByName = await _categoryRepository.CategoryExistsByNameAsync(dto.Name);

            if (categoryExistsByName)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre  '{dto.Name}'");
            }


            //mappear los cambios del dto al modelo/entindad
            _mapper.Map(dto, existingCategory);

            //actualizar la categoria en la base de datos
            var categoryUpdated = await _categoryRepository.UpdateCategoryAsync(existingCategory);

            if (!categoryUpdated)
            {
                throw new InvalidOperationException("Ocurrió un error al actualizar la categoría.");
            }

            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            //verificar si la categoria existe
            var existingCategory = await _categoryRepository.GetCategoryAsync(id);

            if (existingCategory == null)
            {
                throw new InvalidOperationException($"No se encontró la categoría con ID {id}");
            }
            //Borrar la categoria en la base de datos
            var categoryDeleted = await _categoryRepository.DeleteCategoryAsync(id);

            if (!categoryDeleted)
            {
                throw new InvalidOperationException("Ocurrió un error al actualizar la categoría.");
            }

            return categoryDeleted;
        }
    }
}


