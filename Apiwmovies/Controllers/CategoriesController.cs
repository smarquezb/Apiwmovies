using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.P.Movies.Controllers
{
    //controladores 
    [Route("api/[controller]")] //ruta o URL para poder navegar o hacer las consultas
    [ApiController]// Sin este no se puede convertir un controlador en una API
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategoriesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()
        {
            var categoriesDto = await _categoryService.GetCategoriesAsync();
            return Ok(categoriesDto); // Ok significa que la respuesta fue exitosa, http status code 200
        }

        [HttpGet("{id:int}", Name = "GetCategoryAsync")] // Este metodo sigue siendo un Get
        //http status codes
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Informo al cliente que la categoria no fue encontrada

        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)
        {
            var categoryDto = await _categoryService.GetCategoryAsync(id);
            return Ok(categoryDto); // Ok significa que la respuesta fue exitosa, http status code 200
        }

        [HttpPost(Name = "CreateCategoryAsync")] // Este metodo siguien siendo un Get
        //http status codes
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status201Created)] //indica que el recurso fue creado exitosamente
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Indica que hubo un conflicto a la hora de guardar el objeto en la base de datos

        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CategoryCreateUpdateDto categoryCreateDto)
        {
            if (!ModelState.IsValid) // ! significa que me esta negando la condicion
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto);
                //vamos a retornar un 201 created con la ruta para obtener la categoria creada
                return CreatedAtRoute(
                    "GetCategoryAsync",                 //1mer parametro: nombre de la ruta
                    new { id = createdCategory.Id },    //2do parametro: los valores de los parametros de la ruta
                    createdCategory                     //3er parametro: el objeto creado
                    );
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}", Name = "UpdateCategoryAsync")] // Este metodo siguien siendo un Get
        //http status codes
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Indica que hubo un conflicto a la hora de guardar el objeto en la base de datos
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync([FromBody] CategoryCreateUpdateDto dto, int id)
        {
            if (!ModelState.IsValid) // ! significa que me esta negando la condicion
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateCategory = await _categoryService.UpdateCategoryAsync(dto, id);

                return Ok(updateCategory); // Ok significa que la respuesta fue exitosa, http status code 200
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteCategoryAsync")] // Este metodo siguien siendo un Get
        //http status codes
        [ProducesResponseType(StatusCodes.Status204NoContent)] //indica que el resultado es un 200 pero sin contenido
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var deletedCategory = await _categoryService.DeleteCategoryAsync(id);

                return Ok(deletedCategory); // Ok significa que la respuesta fue exitosa, http status code 200
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}