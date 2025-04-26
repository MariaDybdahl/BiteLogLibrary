using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiteLogRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomMealController : ControllerBase
    {
        private readonly CustomMealRepository _customMealRepository;

        public CustomMealController(CustomMealRepository customMealRepository)
        {
            _customMealRepository = customMealRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomMeal>>> Get()
        {
            var customMeal = await _customMealRepository.GetAllAsync();
            return Ok(customMeal);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var customMeal = await _customMealRepository.GetByIdAsync(id);
            return customMeal == null ? NotFound() : Ok(customMeal);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomMeal>> Post([FromBody] CustomMeal customMeal)
        {

            CustomMeal createdCustomMeal = await _customMealRepository.AddAsync(customMeal);
            return Created(
                Url.ActionContext.HttpContext.Request.Path + "/" + createdCustomMeal.Id,
                createdCustomMeal);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomMeal>> Put(int id, [FromBody] CustomMeal customMeal)
        {
            CustomMeal? updatedCustomMeal = await _customMealRepository.UpdateAsync(id, customMeal);
            if (updatedCustomMeal == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(updatedCustomMeal);
            }

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomMeal>> Delete(int id)
        {
            CustomMeal? deletedCustomMeal = await _customMealRepository.DeleteAsync(id);
            if (deletedCustomMeal == null) { return NotFound("No such book, id: " + id); }
            return Ok(deletedCustomMeal);
        }
    }
}
