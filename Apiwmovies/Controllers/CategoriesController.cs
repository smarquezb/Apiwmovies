using Apiwmovies.DAL.Models.Dtos;
using Apiwmovies.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apiwmovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)

        {

            _categoryService = categoryService;

        }

        [HttpGet]


        [ProducesResponseType(StatusCodes.Status200OK)]


        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [ProducesResponseType(StatusCodes.Status400BadRequest)]



        public async Task<ActionResult<ICollection<CategoryDto>>> GetCategoriesAsync()

        {

            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categoriesDto); //http status code 200

        }
    }
}
[HttpGet("{id:int}", Name = "GetCategoryAsync")]

[ProducesResponseType(StatusCodes.Status200OK)]

[ProducesResponseType(StatusCodes.Status500InternalServerError)]

[ProducesResponseType(StatusCodes.Status400BadRequest)]

[ProducesResponseType(StatusCodes.Status404NotFound)]

public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int id)

{

    var categoryDto = await _categoryService.GetCategoryAsync(id);

    return Ok(categoryDto);

}

[HttpGet("{id:int}", Name = "GetCategoryAsync")]

[ProducesResponseType(StatusCodes.Status200OK)]

[ProducesResponseType(StatusCodes.Status500InternalServerError)]

[ProducesResponseType(StatusCodes.Status400BadRequest)]

[ProducesResponseType(StatusCodes.Status404NotFound)]

public async Task<ActionResult<CategoryDto>> CreateCategoryAsync(int id)

{

    var categoryDto = await _categoryService.GetCategoryAsync(id);

    return Ok(categoryDto);
    public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CategoryCreateDto categoryCreateDto)

{

    if (!ModelState.IsValid)

    {

        return BadRequest(ModelState);

    }

``

try

    {

        var createdCategory = await _categoryService.CreateCategoryAsync(categoryCreateDto);

``

//Vamos a retornar un 201 Created con la ruta para obtener la categoría creada

return CreatedAtRoute(

"GetCategoryAsync", //1er parámetro: nombre de la ruta

new { id = createdCategory.Id }, //2do parámetro: los valores de los parámetros de la ruta

createdCategory //3er parámetro: el objeto creado

);

    }

    catch (Exception)

    {

    }

}